namespace Tokens
{
    using Constants;
    using Errors;

    internal static class Keywords{
        public static TokenType StrKWToTType(string input){
            switch(input){
                case KwStrConsts.GLOBAL: return TokenType.GLOBAL;
                case KwStrConsts.NEW: return TokenType.NEW;
                case KwStrConsts.TIME: return TokenType.TIME;
                case KwStrConsts.NATION: return TokenType.NATION;
                case KwStrConsts.SPEED: return TokenType.SPEED;
                case KwStrConsts.DISPLAY: return TokenType.DISPLAY;
                case KwStrConsts.AREA: return TokenType.AREA;
                case KwStrConsts.VIEW: return TokenType.VIEW;
                case KwStrConsts.INFORMATION: return TokenType.INFORMATION;
                case KwStrConsts.ALL: return TokenType.ALL;
                case KwStrConsts.USING: return TokenType.USING;
                case KwStrConsts.FROM: return TokenType.FROM;
                case KwStrConsts.SHIPYARD: return TokenType.SHIPYARD;
                case KwStrConsts.BERTHS: return TokenType.BERTHS;
                case KwStrConsts.MAXSIZE_ALT_SPELLING: case KwStrConsts.MAXSIZE:  return TokenType.MAXSIZE;
                case KwStrConsts.MINSIZE_ALT_SPELLING: case KwStrConsts.MINSIZE:  return TokenType.MINSIZE;
                case KwStrConsts.SHIPCLASS: return TokenType.SHIPCLASS;
                case KwStrConsts.TEMPLATE: return TokenType.TEMPLATE;
                case KwStrConsts.TRUE: return TokenType.TRUE;
                case KwStrConsts.FALSE: return TokenType.FALSE;
                case KwStrConsts.EQUALIZE: return TokenType.EQUALIZE;
                case KwStrConsts.SWITCH: return TokenType.SWITCH;//replaces if
                case KwStrConsts.ELSE: return TokenType.ELSE;
                case KwStrConsts.ADD: return TokenType.ADD;
                case KwStrConsts.REMOVE: return TokenType.REMOVE;
                case KwStrConsts.HOLDQUEUE_ALT_SPELLING: case KwStrConsts.HOLDQUEUE:  return TokenType.HOLDQUEUE;
                case KwStrConsts.RELEASE: return TokenType.RELEASE;
                case KwStrConsts.WHILE: return TokenType.WHILE;
                case KwStrConsts.QUEUELENGTH_ALT_SPELLING: case KwStrConsts.QUEUELENGTH:  return TokenType.QUEUELENGTH;
                case KwStrConsts.MAXQUEUE_ALT_SPELLING: case KwStrConsts.MAXQUEUE:  return TokenType.MAXQUEUE;//seek to maintain minqueue, force maintain maxqueue.
                case KwStrConsts.MINQUEUE_ALT_SPELLING: case KwStrConsts.MINQUEUE:  return TokenType.MINQUEUE;
                case KwStrConsts.MIN: return TokenType.MIN;
                case KwStrConsts.MAX: return TokenType.MAX;
                case KwStrConsts.NOT: return TokenType.NOT;
                case KwStrConsts.SHIFT: return TokenType.SHIFT;
                case KwStrConsts.UNSHIFT: return TokenType.UNSHIFT;
                case KwStrConsts.AND: return TokenType.AND;
                case KwStrConsts.ROUND: return TokenType.ROUND;
                case KwStrConsts.PERMANENT: return TokenType.PERMANENT;
                case KwStrConsts.OR: return TokenType.OR;
                case KwStrConsts.GET: return TokenType.GET;
                case KwStrConsts.CASE: return TokenType.CASE;
                case KwStrConsts.BREAK: return TokenType.BREAK;
                case KwStrConsts.FUNCTION: return TokenType.FUNCTION;
                case KwStrConsts.COMMENT: return TokenType.COMMENT;
                case KwStrConsts.MODIFY: return TokenType.MODIFY;
                case KwStrConsts.INT: return TokenType.INT;
                case KwStrConsts.STRING: return TokenType.STRING;
                case KwStrConsts.COMPONENT: return TokenType.COMPONENT;
                case KwStrConsts.ALIAS: return TokenType.ALIAS;
                case KwStrConsts.DEPENDANCY: return TokenType.DEPENDANCY;
                case KwStrConsts.OBJECT_TIME: return TokenType.OBJECT_TIME;
                case KwStrConsts.IF: return TokenType.IF;
                case KwStrConsts.FOR: return TokenType.FOR;
                case KwStrConsts.FOREACH: return TokenType.FOREACH;
                case KwStrConsts.IN: return TokenType.IN;
                case KwStrConsts.QUEUE: return TokenType.QUEUE;
                case KwStrConsts.VOID: return TokenType.VOID;
                case KwStrConsts.NAMESPACE: return TokenType.NAMESPACE;
                case KwStrConsts.LENGTH: return TokenType.LENGTH;
                case KwStrConsts.TYPE: return TokenType.TYPE;
                default: return TokenType.INTERPRETERNULL;

            }
        }
        
        }
        
    
    internal static class Is{
        public static bool Switch(TokenType input){
            switch(input){
                case TokenType.INFORMATION: 
                case TokenType.ALL:
                case TokenType.FROM:
                case TokenType.IN:
                    return true;
                default: return false;
            }
        }
        public static bool ValueType(TokenType input){
            switch(input){
                case TokenType.GLOBAL:
                case TokenType.TIME:
                case TokenType.NATION:
                case TokenType.AREA:
                case TokenType.SHIPYARD:
                case TokenType.BERTHS:
                case TokenType.SHIPCLASS:
                case TokenType.TEMPLATE:
                case TokenType.FUNCTION:
                case TokenType.INT:
                case TokenType.STRING:
                case TokenType.COMPONENT:
                case TokenType.VOID:
                case TokenType.NAMESPACE:
                    return true;
                default: return false;
            }
        }
        public static bool NameSpaceType(TokenType input){
            switch(input){
                case TokenType.GLOBAL:
                case TokenType.NATION:
                case TokenType.AREA:
                case TokenType.TEMPLATE:
                case TokenType.FUNCTION:
                    return true;
                default: return false;
            }
        }
        public static bool Command(TokenType input){
            switch(input){
                case TokenType.NEW:
                case TokenType.VIEW:
                case TokenType.USING:
                case TokenType.PERMANENT:
                case TokenType.BREAK:
                case TokenType.MODIFY:
                case TokenType.FOR:
                case TokenType.FOREACH:
                    return true;
                default: return false;
            }
        }
        public static bool Parameter(TokenType input){
            switch(input){
                case TokenType.MAXQUEUE:
                case TokenType.MINQUEUE:
                case TokenType.MAXSIZE:
                case TokenType.MINSIZE:
                case TokenType.SPEED:
                case TokenType.DISPLAY:
                case TokenType.EQUALIZE:
                case TokenType.COMMENT:
                case TokenType.DEPENDANCY:
                    return true;
                default: return false;
            }
        }
        public static bool Ctrl(TokenType input){
            switch(input){
                case TokenType.SWITCH:
                case TokenType.ELSE:
                case TokenType.WHILE:
                case TokenType.NOT:
                case TokenType.AND:
                case TokenType.OR:
                case TokenType.CASE:
                case TokenType.IF:
                    return true;
                default: return false;
            }
        }
        public static bool Function(TokenType input){
            switch(input){
                case TokenType.ADD:
                case TokenType.REMOVE:
                case TokenType.RELEASE:
                case TokenType.HOLDQUEUE:
                case TokenType.MIN:
                case TokenType.MAX:
                case TokenType.SHIFT:
                case TokenType.UNSHIFT:
                case TokenType.ROUND:
                case TokenType.GET:
                    return true;
                default: return false;
            }
        }
        public static bool Attribute(TokenType input){
            switch(input){
                case TokenType.QUEUE:
                case TokenType.QUEUELENGTH:
                    return true;
                default: return false;
            }
        }
        public static bool CtrlOperator(TokenType input){
            switch(input){
                case TokenType.NOT:
                case TokenType.AND:
                case TokenType.OR:
                    return true;
                default: return false;
            }
        }
    }
    internal enum TokenType{
        DEPENDANCY,
        GLOBAL,
        NEW,
        TIME,
        LEFTPAREN,
        RIGHTPAREN,
        SEMICOLON,
        NATION,
        LEFTBRACE,
        RIGHTBRACE,
        SPEED,
        DISPLAY,
        COMMENT,
        AREA,
        DOT,
        STRING,
        IDENTIFIER,
        NUMBER,
        VIEW,
        INFORMATION,
        PROGRAM,
        ALL,
        USING,
        FROM,
        SHIPYARD,
        BERTHS,
        MAXSIZE,
        MINSIZE,
        SHIPCLASS,
        COLON,
        TEMPLATE,
        TRUE,
        FALSE,
        EQUALIZE,
        IF,
        ELSE,
        ADD,
        REMOVE,
        HOLDQUEUE,
        RELEASE,
        WHILE,
        QUEUELENGTH,
        MINQUEUE,
        MAXQUEUE,
        MIN,
        MAX,
        PLUS,
        MINUS,
        NOT,
        STAR,
        SLASH,
        DOUBLEEQUALS,
        PERCENT,
        SHIFT,
        UNSHIFT,
        AND,
        REPEAT,
        ROUND,
        PERMANENT,
        EQUALS,
        OR,
        EOF,
        GET,
        SWITCH,
        CASE,
        BREAK,
        FUNCTION,
        MODIFY,
        IPARENT,
        MONTHTIME,
        INT,
        STRINGVAR,
        BOOL,
        LEFTSQUAREBRACKET,
        RIGHTSQUAREBRACKET,
        COMPONENT,
        ALIAS,
        COMMA,
        OBJECT_TIME,
        FOR,
        FOREACH,
        IN,
        DOUBLE_PLUS,
        DOUBLE_MINUS,
        DOUBLE_STAR,
        QUEUE,//optinal qualifier for accessing queue.
        VOID,
        NAMESPACE,
        LENGTH,
        TYPE,
        //Interpreter only Tokens:
        INTERPRETERNULL,
    }
    //implement arguments, round(true, false);
    internal enum TokenTypeType{//type of TokenType
        FUNCTION,
        SYMBOL,
        VALUETYPE,
        NAMESPACETYPE,
        VALUE,
        COMMAND,
        PARAMETER,
        IDENTIFIER,
        SWITCH,
        CTRL,
        ATTRIBUTE,
        CTRL_OPERATOR,
        OPERATOR,
        INTERPRETERNULL
    }
    internal readonly struct Token{
        public TokenType Type{ get; init; }
        public int Line{ get; init; }
        public string Lexeme{ get; init; }
        public string? Literal{ get; init; }
        public Token(TokenType aTokenType, int aLine, string aLexeme, string? aLiteral){
            Line = aLine;
            Lexeme = aLexeme;
            Literal = aLiteral;
            Type = aTokenType;
        }
        public override string ToString(){
            return Lexeme;
        }
    }
}
