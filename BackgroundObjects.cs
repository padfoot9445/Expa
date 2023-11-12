namespace BackgroundObjects{
    using Errors;
    using Tokens;
    using ExpaObjects;
    using Parser;
    using Structs;
    using Interfaces;

    public class StaticCommands{
        public void Execute(){
            
        }
        public StaticCommands(){
            throw new NotImplementedException();
        }
    }
    public abstract class BaseObject{
        public Token TokenIdentifier;
        public HashSet<string> parents = new();
        public string display;
        public string? comment;

        public BaseObject(BaseNameSpace parent, Token identifier, string? display=null, string? comment = null){
            TokenIdentifier = identifier;
            parents.Add(parent.TokenIdentifier.lexeme);
            this.display = display ?? identifier.lexeme;
            this.comment = comment;
        }
        public BaseObject(Token identifier, string? display=null, string? comment = null){
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
            if (!((BaseNameSpace)Parser.expaObjects[parent]).children.Contains(TokenIdentifier.lexeme)){
                //if the parent does not have the current object as a child
                ((BaseNameSpace)Parser.expaObjects[parent]).AddChild(TokenIdentifier.lexeme);
            }
            return true;
                
        }
    }
    public abstract class BaseNameSpace: BaseObject{
        public Scope scope;
        public HashSet<string> children = new();
        public BaseNameSpace(BaseNameSpace parent, Scope aScope, string? display=null, string? comment = null):base(parent, aScope.TokenIdentifier, display, comment){
            scope = aScope;
        }
        public BaseNameSpace(Scope aScope, string? display=null, string? comment = null): base(aScope.TokenIdentifier, display, comment){
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
    //*Values
    /*#region Values*/
    public class BackgroundTime{
        //parse only mm/yy or yy.q
        public readonly int year;
        public readonly int quarter;
        public readonly int month;
        public BackgroundTime(int year, int quarter){
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
        public static BackgroundTime MonthTime(int year, int month){
            return new BackgroundTime(year, MonthToQuarter(month));
        }
        public static BackgroundTime operator+ (BackgroundTime self, BackgroundTime other){
            (int newYear, int newQuarter) = WrapTime(self.year + other.year, self.quarter + other.quarter);
            return new BackgroundTime(newYear, newQuarter);
        }
        public static BackgroundTime operator- (BackgroundTime self, BackgroundTime other){
            (int newYear, int newQuarter) = WrapTime(self.year - other.year, self.quarter - other.quarter);
            return new BackgroundTime(newYear, newQuarter);
        }
        public static BackgroundTime operator* (BackgroundTime self, int other){
           (int year, int quarter) = WrapTime((int)Math.Round((double)self.year * other), (int)Math.Round((double)self.quarter * other));
           return new BackgroundTime(year, quarter);
        }
        public static BackgroundTime operator /(BackgroundTime self, int other) => self * (1 / other);
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
        public static BackgroundTime ParseAcTime(string input){
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
        public static BackgroundTime ParseMonthTime(string input){
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
                    return BackgroundTime.MonthTime(int.Parse(parts[0]), int.Parse(parts[1]));
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
    public class BackgroundNumber : IBackgroundValue{
        public string IntegerValue;
        public string? DecimalValue;
        public BackgroundNumber(string IntegerValue, string? DecimalValue){
            this.IntegerValue = IntegerValue;
            this.DecimalValue = DecimalValue;
        }
        public bool ToBool(){
            
        }
        private static int ToNum(string IntegerValue) => int.Parse(IntegerValue);
        private static float ToNum(string IntegerValue, string DecimalValue) => float.Parse($"{IntegerValue}.{DecimalValue}");
    }
}