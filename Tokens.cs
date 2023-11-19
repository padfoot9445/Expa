namespace Tokens
{
    using Constants;
    using Errors;

    internal static class Keywords{
        public static TokenType StrKWToTType(string input){
            switch(input){
                case KeywordStringConstants.GLOBAL: return TokenType.GLOBAL;
                case KeywordStringConstants.NEW: return TokenType.NEW;
                case KeywordStringConstants.TIME: return TokenType.TIME;
                case KeywordStringConstants.NATION: return TokenType.NATION;
                case KeywordStringConstants.SPEED: return TokenType.SPEED;
                case KeywordStringConstants.DISPLAY: return TokenType.DISPLAY;
                case KeywordStringConstants.AREA: return TokenType.AREA;
                case KeywordStringConstants.VIEW: return TokenType.VIEW;
                case KeywordStringConstants.INFORMATION: return TokenType.INFORMATION;
                case KeywordStringConstants.ALL: return TokenType.ALL;
                case KeywordStringConstants.USING: return TokenType.USING;
                case KeywordStringConstants.FROM: return TokenType.FROM;
                case KeywordStringConstants.SHIPYARD: return TokenType.SHIPYARD;
                case KeywordStringConstants.BERTHS: return TokenType.BERTHS;
                case KeywordStringConstants.MAXSIZE_ALT_SPELLING: case KeywordStringConstants.MAXSIZE:  return TokenType.MAXSIZE;
                case KeywordStringConstants.MINSIZE_ALT_SPELLING: case KeywordStringConstants.MINSIZE:  return TokenType.MINSIZE;
                case KeywordStringConstants.SHIPCLASS: return TokenType.SHIPCLASS;
                case KeywordStringConstants.TEMPLATE: return TokenType.TEMPLATE;
                case KeywordStringConstants.TRUE: return TokenType.TRUE;
                case KeywordStringConstants.FALSE: return TokenType.FALSE;
                case KeywordStringConstants.EQUALIZE: return TokenType.EQUALIZE;
                case KeywordStringConstants.SWITCH: return TokenType.SWITCH;//replaces if
                case KeywordStringConstants.ELSE: return TokenType.ELSE;
                case KeywordStringConstants.ADD: return TokenType.ADD;
                case KeywordStringConstants.REMOVE: return TokenType.REMOVE;
                case KeywordStringConstants.HOLDQUEUE_ALT_SPELLING: case KeywordStringConstants.HOLDQUEUE:  return TokenType.HOLDQUEUE;
                case KeywordStringConstants.RELEASE: return TokenType.RELEASE;
                case KeywordStringConstants.WHILE: return TokenType.WHILE;
                case KeywordStringConstants.QUEUELENGTH_ALT_SPELLING: case KeywordStringConstants.QUEUELENGTH:  return TokenType.QUEUELENGTH;
                case KeywordStringConstants.MAXQUEUE_ALT_SPELLING: case KeywordStringConstants.MAXQUEUE:  return TokenType.MAXQUEUE;//seek to maintain minqueue, force maintain maxqueue.
                case KeywordStringConstants.MINQUEUE_ALT_SPELLING: case KeywordStringConstants.MINQUEUE:  return TokenType.MINQUEUE;
                case KeywordStringConstants.MIN: return TokenType.MIN;
                case KeywordStringConstants.MAX: return TokenType.MAX;
                case KeywordStringConstants.NOT: return TokenType.NOT;
                case KeywordStringConstants.SHIFT: return TokenType.SHIFT;
                case KeywordStringConstants.UNSHIFT: return TokenType.UNSHIFT;
                case KeywordStringConstants.AND: return TokenType.AND;
                case KeywordStringConstants.ROUND: return TokenType.ROUND;
                case KeywordStringConstants.PERMANENT: return TokenType.PERMANENT;
                case KeywordStringConstants.OR: return TokenType.OR;
                case KeywordStringConstants.GET: return TokenType.GET;
                case KeywordStringConstants.CASE: return TokenType.CASE;
                case KeywordStringConstants.BREAK: return TokenType.BREAK;
                case KeywordStringConstants.FUNCTION: return TokenType.FUNCTION;
                case KeywordStringConstants.COMMENT: return TokenType.COMMENT;
                case KeywordStringConstants.MODIFY: return TokenType.MODIFY;
                case KeywordStringConstants.INT: return TokenType.INT;
                case KeywordStringConstants.STRING: return TokenType.STRING;
                case KeywordStringConstants.COMPONENT: return TokenType.COMPONENT;
                case KeywordStringConstants.ALIAS: return TokenType.ALIAS;
                case KeywordStringConstants.DEPENDANCY: return TokenType.DEPENDANCY;
                case KeywordStringConstants.OBJECT_TIME: return TokenType.OBJECT_TIME;
                case KeywordStringConstants.IF: return TokenType.IF;
                case KeywordStringConstants.FOR: return TokenType.FOR;
                case KeywordStringConstants.FOREACH: return TokenType.FOREACH;
                case KeywordStringConstants.IN: return TokenType.IN;
                case KeywordStringConstants.QUEUE: return TokenType.QUEUE;
                default: return TokenType.INTERPRETERNULL;

            }
        }
        public static TokenTypeType IdentifierToTTypeType(string input){
            switch(input){
                case KeywordStringConstants.GLOBAL: return TokenTypeType.NAMESPACETYPE;
                case KeywordStringConstants.NEW: return TokenTypeType.NAMESPACETYPE;
                case KeywordStringConstants.TIME: return TokenTypeType.VALUETYPE;
                case KeywordStringConstants.NATION: return TokenTypeType.NAMESPACETYPE;
                case KeywordStringConstants.SPEED: return TokenTypeType.PARAMETER;
                case KeywordStringConstants.DISPLAY: return TokenTypeType.PARAMETER;
                case KeywordStringConstants.AREA: return TokenTypeType.NAMESPACETYPE;
                case KeywordStringConstants.VIEW: return TokenTypeType.COMMAND;
                case KeywordStringConstants.INFORMATION: return TokenTypeType.SWITCH;
                case KeywordStringConstants.ALL: return TokenTypeType.SWITCH;
                case KeywordStringConstants.USING: return TokenTypeType.COMMAND;
                case KeywordStringConstants.FROM: return TokenTypeType.SWITCH;
                case KeywordStringConstants.SHIPYARD: return TokenTypeType.VALUETYPE;
                case KeywordStringConstants.BERTHS: return TokenTypeType.VALUETYPE;
                case KeywordStringConstants.MAXSIZE_ALT_SPELLING: case KeywordStringConstants.MAXSIZE:  return TokenTypeType.PARAMETER;
                case KeywordStringConstants.MINSIZE_ALT_SPELLING: case KeywordStringConstants.MINSIZE:  return TokenTypeType.PARAMETER;
                case KeywordStringConstants.SHIPCLASS: return TokenTypeType.VALUETYPE;
                case KeywordStringConstants.TEMPLATE: return TokenTypeType.NAMESPACETYPE;
                case KeywordStringConstants.TRUE: return TokenTypeType.VALUE;
                case KeywordStringConstants.FALSE: return TokenTypeType.VALUE;
                case KeywordStringConstants.EQUALIZE: return TokenTypeType.PARAMETER;
                case KeywordStringConstants.SWITCH: return TokenTypeType.CTRL;//replaces if
                case KeywordStringConstants.ELSE: return TokenTypeType.CTRL;
                case KeywordStringConstants.ADD: return TokenTypeType.FUNCTION;
                case KeywordStringConstants.REMOVE: return TokenTypeType.FUNCTION;
                case KeywordStringConstants.HOLDQUEUE_ALT_SPELLING: case KeywordStringConstants.HOLDQUEUE:  return TokenTypeType.FUNCTION;
                case KeywordStringConstants.RELEASE: return TokenTypeType.FUNCTION;
                case KeywordStringConstants.WHILE: return TokenTypeType.CTRL;
                case KeywordStringConstants.QUEUELENGTH_ALT_SPELLING: case KeywordStringConstants.QUEUELENGTH:  return TokenTypeType.ATTRIBUTE;
                case KeywordStringConstants.MAXQUEUE_ALT_SPELLING: case KeywordStringConstants.MAXQUEUE:  return TokenTypeType.PARAMETER;
                case KeywordStringConstants.MINQUEUE_ALT_SPELLING: case KeywordStringConstants.MINQUEUE:  return TokenTypeType.PARAMETER;
                case KeywordStringConstants.MIN: return TokenTypeType.FUNCTION;
                case KeywordStringConstants.MAX: return TokenTypeType.FUNCTION;
                case KeywordStringConstants.NOT: return TokenTypeType.CTRL_OPERATOR;
                case KeywordStringConstants.SHIFT: return TokenTypeType.FUNCTION;
                case KeywordStringConstants.UNSHIFT: return TokenTypeType.FUNCTION;
                case KeywordStringConstants.AND: return TokenTypeType.CTRL_OPERATOR;
                case KeywordStringConstants.ROUND: return TokenTypeType.FUNCTION;
                case KeywordStringConstants.PERMANENT: return TokenTypeType.COMMAND;
                case KeywordStringConstants.OR: return TokenTypeType.CTRL_OPERATOR;
                case KeywordStringConstants.GET: return TokenTypeType.FUNCTION;
                case KeywordStringConstants.CASE: return TokenTypeType.CTRL;
                case KeywordStringConstants.BREAK: return TokenTypeType.COMMAND;
                case KeywordStringConstants.FUNCTION: return TokenTypeType.NAMESPACETYPE;
                case KeywordStringConstants.COMMENT: return TokenTypeType.PARAMETER;
                case KeywordStringConstants.MODIFY: return TokenTypeType.COMMAND;
                case KeywordStringConstants.INT: return TokenTypeType.VALUETYPE;
                case KeywordStringConstants.STRING: return TokenTypeType.VALUETYPE;
                case KeywordStringConstants.COMPONENT: return TokenTypeType.VALUETYPE;
                case KeywordStringConstants.ALIAS: return TokenTypeType.VALUE;
                case KeywordStringConstants.DEPENDANCY: return TokenTypeType.PARAMETER;
                case KeywordStringConstants.IF: return TokenTypeType.CTRL;
                case KeywordStringConstants.FOR: return TokenTypeType.COMMAND;
                case KeywordStringConstants.FOREACH: return TokenTypeType.COMMAND;
                case KeywordStringConstants.IN: return TokenTypeType.COMMAND;
                case KeywordStringConstants.QUEUE: return TokenTypeType.ATTRIBUTE;
                default: throw new ExpaInterpreterError(-1, "Reached default case in IdentifierToTypeType conversion.");
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
        public TokenTypeType TypeOfTType{ get; init; }
        public Token(TokenType aTokenType, TokenTypeType tokenTypeType, int aLine, string aLexeme, string? aLiteral){
            Line = aLine;
            Lexeme = aLexeme;
            Literal = aLiteral;
            Type = aTokenType;
            TypeOfTType = tokenTypeType;
        }
        public override string ToString(){
            return Lexeme;
        }
    }
}