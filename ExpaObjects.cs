namespace ExpaObjects{
    using Tokens;
    using Scope;
    using BackgroundObjects;
    public class ExpaObject{
        public Token TokenIdentifier;
        public Dictionary<string, ExpaNameSpace> parents = new();
        public ExpaObject(ExpaNameSpace parent, Token identifier){
            TokenIdentifier = identifier;
            parents[parent.scope.TokenIdentifier.lexeme] = parent;
        }
        public ExpaObject(Token identifier){
            TokenIdentifier = identifier;
        }
        
        public bool AddParent(ExpaNameSpace parent){
            bool rv;
            if(parents[parent.scope.TokenIdentifier.lexeme] != parent){
                //raise syntax warning on caller end, but still do the assignment
                rv = false;
            } else{
                rv = true;
            }
            parents[parent.TokenIdentifier.lexeme] = parent;
            if (!parent.children.ContainsKey(TokenIdentifier.lexeme)){
                //if the parent does not have the current object as a child
                parents[parent.TokenIdentifier.lexeme].AddChild(this);
            }
            return rv;
                
        }
    }
    
    public class ExpaNameSpace: ExpaObject{
        public Scope scope;
        public Dictionary <string, ExpaObject> children = new();
        public ExpaNameSpace(ExpaNameSpace parent, Scope aScope):base(parent, aScope.TokenIdentifier){
            scope = aScope;
        }
        public ExpaNameSpace(Scope aScope): base(aScope.TokenIdentifier){
            scope = aScope;
        }
        public bool AddChild(ExpaObject child){
            bool rv;
            if(children[child.TokenIdentifier.lexeme] != child){
                //if the identifier is a child and the child is different
                rv = false;
            } else{
                rv = true;
            }
            children[child.TokenIdentifier.lexeme] = child;
            return rv;
        }
        public bool AddChild(ExpaNameSpace child){
            bool rv;
            children[child.TokenIdentifier.lexeme] = child;
            if(children[child.TokenIdentifier.lexeme] != child){
                rv = false;
            } else{
                rv = true;
            }
            if (!children[child.TokenIdentifier.lexeme].parents.ContainsKey(scope.TokenIdentifier.lexeme)){
                children[child.TokenIdentifier.lexeme].AddParent(this);
            } 
            return rv;
        }
        public bool addUnparsedChild(){
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

        public ExpaTemplate(ExpaNameSpace parent, Scope scope, ExpaNation nation, bool equalize): base(parent, scope){
            this.nation = nation;
            this.equalize = equalize;
        }
    }

    public class ExpaNation: ExpaNameSpace{
        public Time time;
        public ExpaNation(ExpaNameSpace parent, Scope scope, Time time):base(parent, scope){//handle implicit time on caller side; Global gets its time by modify and not set.
            this.time = time;
        }
    }
    public class ExpaGlobal : ExpaNameSpace{
        public Time time = new(0, 0);
        public ExpaGlobal(Scope scope): base(scope){}
    }
}

namespace BackgroundObjects{
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