using Errors;

namespace ExpaObjects{
    using Tokens;
    using Structs;
    using BackgroundObjects;
    using System.Data.Common;
    using System.Runtime.Serialization;
    using Helpers;
    using Parser;

    public abstract class ExpaObject{
        public Token TokenIdentifier;
        public HashSet<string> parents = new();
        public string display;
        public string? comment;

        public ExpaObject(ExpaNameSpace parent, Token identifier, string? display=null, string? comment = null){
            TokenIdentifier = identifier;
            parents.Add(parent.TokenIdentifier.lexeme);
            this.display = display ?? identifier.lexeme;
            this.comment = comment;
        }
        public ExpaObject(Token identifier, string? display=null, string? comment = null){
            TokenIdentifier = identifier;
            display = identifier.lexeme;
            this.display = display ?? identifier.lexeme;
            this.comment = comment;
        }
        
        public bool AddParent(string parent){
            if(parents.Contains(parent)){
                return false;
            }
            parents.Add(parent);
            if (!((ExpaNameSpace)Parser.expaObjects[parent]).children.Contains(TokenIdentifier.lexeme)){
                //if the parent does not have the current object as a child
                ((ExpaNameSpace)Parser.expaObjects[parent]).AddChild(TokenIdentifier.lexeme);
            }
            return true;
                
        }
    }
    
    public class ExpaNameSpace: ExpaObject{
        public Scope scope;
        public HashSet<string> children = new();
        public ExpaNameSpace(ExpaNameSpace parent, Scope aScope, string? display=null, string? comment = null):base(parent, aScope.TokenIdentifier, display, comment){
            scope = aScope;
        }
        public ExpaNameSpace(Scope aScope, string? display=null, string? comment = null): base(aScope.TokenIdentifier, display, comment){
            scope = aScope;
        }
        public bool AddChild(string child){
            if(children.Contains(child)){
                return false;
            }
            children.Add(child);
            if (!Parser.expaObjects[child].parents.Contains(TokenIdentifier.lexeme)){
                Parser.expaObjects[child].AddParent(TokenIdentifier.lexeme);
            } 
            return true;
        }

    }

    public class ExpaTemplate: ExpaNameSpace{
        public ExpaNation nation;
        public bool equalize;
        static readonly HashSet<TokenType> allowedKw = new(){
            TokenType.EQUALIZE,
            TokenType.NATION
        };

        public ExpaTemplate(ExpaNameSpace parent, Scope scope, ExpaNation nation, bool equalize, string? display=null, string? comment = null): base(parent, scope, display, comment){
            this.nation = nation;
            this.equalize = equalize;
        }
    }

    public class ExpaNation: ExpaNameSpace, IHasTime, ICanBeParent<ExpaNation>, ICanBeParent<ExpaArea>, IHasMinMaxSize{
        public Time Time{get; private set;}
        public int MaxChildShipSize{get; set;}
        public int MinChildShipSize{get; set;}

        public ExpaNation(ICanBeParent<ExpaNation> parent, Scope scope, Time time, int MinChildShipSize=Helpers.Defaults.MINCSS, int MaxChildShipSize=Defaults.MAXCSS,string? display=null, string? comment = null):base((ExpaNameSpace)parent, scope, display, comment){//handle implicit time on caller side; Global gets its time by modify and not set.
            this.Time = time;
            this.MinChildShipSize = MinChildShipSize;
            this.MaxChildShipSize = MaxChildShipSize;
        }
    }
    public class ExpaGlobal : ExpaNameSpace, IHasTime, ICanBeParent<ExpaNation>{
        public Time Time{get; private set;}
        public ExpaGlobal(Scope scope, string? display=null, string? comment = null): base(scope, display, comment){
            Time = new(0, 0);
        }
        public ExpaGlobal(Scope scope, Time time,string? display=null, string? comment = null): base(scope, display, comment){
            Time = time;
        }
        public ExpaGlobal(Scope scope, Time time): base(scope){
            this.Time = time;
        }
    }
    public class ExpaArea : ExpaNameSpace, ICanBeParent<ExpaArea>, IHasMinMaxSize{
        public ExpaNation mainNationParent;
        public int MinChildShipSize{get; set;}
        public int MaxChildShipSize{get; set;}
        public ExpaArea(ICanBeParent<ExpaArea> parent, ExpaNation mainNationParent, int minCSS, int maxCSS,Scope aScope, string? display = null, string? comment = null) : base((ExpaNameSpace)parent, aScope, display, comment){
            this.mainNationParent = mainNationParent;
            this.MinChildShipSize = minCSS;
            this.MaxChildShipSize = maxCSS;
        }
        
    }
    public class ExpaTime: ExpaObject,IHasTime{
        public Time Time{get; private set;}
        public ExpaTime(ExpaNameSpace parent, Token identifier, Time time, string? display = null, string? comment = null) : base(parent, identifier, display, comment){
            Time = time;
        }
    }

        
        
}

namespace BackgroundObjects{
    public interface IHasTime{
        public Time Time{get;}
    }
    public class Time{
        //parse only mm/yy or yy.q
        public readonly int year;
        public readonly int quarter;
        public readonly int month;
        public Time(int year, int quarter){
            this.year = year;
            this.quarter = quarter;
            month = QuarterToMonth(quarter);
        }
        private static int MonthToQuarter(int month){
            return (int)((month + 2) / 3);
        }

        private static int QuarterToMonth(int quarter){
            return (int)(quarter * 3 - 2);
        }
        public static Time MonthTime(int year, int month){
            return new Time(year, MonthToQuarter(month));
        }
        public static Time operator+ (Time self, Time other){
            (int newYear, int newQuarter) = WrapTime(self.year + other.year, self.quarter + other.quarter);
            return new Time(newYear, newQuarter);
        }
        public static Time operator- (Time self, Time other){
            (int newYear, int newQuarter) = WrapTime(self.year - other.year, self.quarter - other.quarter);
            return new Time(newYear, newQuarter);
        }
        public static Time operator* (Time self, int other){
           (int year, int quarter) = WrapTime((int)Math.Round((double)self.year * other), (int)Math.Round((double)self.quarter * other));
           return new Time(year, quarter);
        }
        public static Time operator /(Time self, int other) => self * (1 / other);
        public string ToString(bool MonthTime)
        {
            return MonthTime? $"{year}/{month}" : $"{year}.{quarter}";
        }
        public override string ToString()
        {
            return ToString(false);
        }
        ///<summary>
        ///returns a new Time object with the given Quarter Notation AC time string input
        ///</summary>
        ///<param name="input"> the string containing Quarter Notation AC time</param>
        public static Time ParseAcTime(string input){
            string[] parts = input.Split('.');
            if(parts.Length > 2 || parts.Length < 1){
                throw new MainException($"{input} was an invalid AC time format");
            } else if(parts.Length == 1){
                try{
                    return new(int.Parse(parts[0]), 0);
                } catch(FormatException){
                    throw new MainException($"{input} was an invalid AC time format - could not split into year and month || invalid year");
                }
            } else{
                try{
                    return new(int.Parse(parts[0]), int.Parse(parts[1]));
                } catch(FormatException){
                    throw new MainException($"{input} was an invalid AC time format - either month or year could not be converted into an int");
                }
            }
        }
        public static Time ParseMonthTime(string input){
            string[] parts = input.Split('/');
            if(parts.Length > 2 || parts.Length < 1){
                throw new MainException($"{input} was an invalid Month AC time format");
            } else if(parts.Length == 1){
                try{
                    return new(int.Parse(parts[0]), 0);
                } catch(FormatException){
                    throw new MainException($"{input} was an invalid Month AC time format - could not split into year and month || invalid year");
                } 
            } else{
                try{
                    return Time.MonthTime(int.Parse(parts[0]), int.Parse(parts[1]));
                } catch(FormatException){
                    throw new MainException($"{input} was an invalid Month AC time format - either month or year could not be converted into an int");
                }
            }
        }
        private static (int, int) WrapTime(int year, int quarter){
            while(quarter > 4){
                quarter -= 4;
                year++;
            }
            while(quarter < 0){
                quarter += 4;
                year--;
            }
            return (year, quarter);
        }
    }
    public interface ICanBeParent<T>{}
    public interface IHasMaxSize{
        public int MaxChildShipSize{get; set;}
    }
    public interface IHasMinSize{
        public int MinChildShipSize{get; set;}
    }
    public interface IHasMinMaxSize: IHasMaxSize, IHasMinSize{}
}