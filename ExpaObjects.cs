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
        public readonly double objectID; 

        public ExpaObject(ExpaNameSpace parent, Token identifier, string? display=null, string? comment = null){
            TokenIdentifier = identifier;
            parents.Add(parent.TokenIdentifier.lexeme);
            this.display = display ?? identifier.lexeme;
            this.comment = comment;
            this.objectID = UID.New(identifier.lexeme, parent.TokenIdentifier.lexeme);
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

    public class ExpaNation: ExpaNameSpace, IHasTime{
        public Time Time{get; private set;}
        public ExpaNation(ExpaNameSpace parent, Scope scope, Time time, string? display=null, string? comment = null):base(parent, scope, display, comment){//handle implicit time on caller side; Global gets its time by modify and not set.
            this.Time = time;
        }
    }
    public class ExpaGlobal : ExpaNameSpace, IHasTime{
        public Time Time{get; private set;}
        public ExpaGlobal(Scope scope, string? display=null, string? comment = null): base(scope, display, comment){
            Time = new(0, 0);
        }
        public ExpaGlobal(Scope scope, Time time): base(scope){
            this.Time = time;
        }
    }
}

namespace BackgroundObjects{
    interface IHasTime{
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
}