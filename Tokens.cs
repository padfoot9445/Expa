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
                case KeywordStringConstants.VOID: return TokenType.VOID;
                case KeywordStringConstants.NAMESPACE: return TokenType.NAMESPACE;
                default: return TokenType.INTERPRETERNULL;

            }
        }
        public static TokenTypeType[] IdentifierToTTypeType(string input){
            switch(input){
                case KeywordStringConstants.GLOBAL: return new TokenTypeType[]{TokenTypeType.VALUETYPE, TokenTypeType.NAMESPACETYPE};
                case KeywordStringConstants.NEW: return new TokenTypeType[]{TokenTypeType.COMMAND};
                case KeywordStringConstants.TIME: return new TokenTypeType[]{TokenTypeType.VALUETYPE};
                case KeywordStringConstants.NATION: return new TokenTypeType[]{TokenTypeType.VALUETYPE, TokenTypeType.NAMESPACETYPE};
                case KeywordStringConstants.SPEED: return new TokenTypeType[]{TokenTypeType.PARAMETER};
                case KeywordStringConstants.DISPLAY: return new TokenTypeType[]{TokenTypeType.PARAMETER};
                case KeywordStringConstants.AREA: return new TokenTypeType[]{TokenTypeType.VALUETYPE, TokenTypeType.NAMESPACETYPE};
                case KeywordStringConstants.VIEW: return new TokenTypeType[]{TokenTypeType.COMMAND};
                case KeywordStringConstants.INFORMATION: return new TokenTypeType[]{TokenTypeType.SWITCH};
                case KeywordStringConstants.ALL: return new TokenTypeType[]{TokenTypeType.SWITCH};
                case KeywordStringConstants.USING: return new TokenTypeType[]{TokenTypeType.COMMAND};
                case KeywordStringConstants.FROM: return new TokenTypeType[]{TokenTypeType.SWITCH};
                case KeywordStringConstants.SHIPYARD: return new TokenTypeType[]{TokenTypeType.VALUETYPE};
                case KeywordStringConstants.BERTHS: return new TokenTypeType[]{TokenTypeType.VALUETYPE};
                case KeywordStringConstants.MAXSIZE_ALT_SPELLING: case KeywordStringConstants.MAXSIZE:  return new TokenTypeType[]{TokenTypeType.PARAMETER};
                case KeywordStringConstants.MINSIZE_ALT_SPELLING: case KeywordStringConstants.MINSIZE:  return new TokenTypeType[]{TokenTypeType.PARAMETER};
                case KeywordStringConstants.SHIPCLASS: return new TokenTypeType[]{TokenTypeType.VALUETYPE};
                case KeywordStringConstants.TEMPLATE: return new TokenTypeType[]{TokenTypeType.VALUETYPE, TokenTypeType.NAMESPACETYPE};
                case KeywordStringConstants.TRUE: return new TokenTypeType[]{TokenTypeType.VALUE};
                case KeywordStringConstants.FALSE: return new TokenTypeType[]{TokenTypeType.VALUE};
                case KeywordStringConstants.EQUALIZE: return new TokenTypeType[]{TokenTypeType.PARAMETER};
                case KeywordStringConstants.SWITCH: return new TokenTypeType[]{TokenTypeType.CTRL};//replaces if
                case KeywordStringConstants.ELSE: return new TokenTypeType[]{TokenTypeType.CTRL};
                case KeywordStringConstants.ADD: return new TokenTypeType[]{TokenTypeType.FUNCTION};
                case KeywordStringConstants.REMOVE: return new TokenTypeType[]{TokenTypeType.FUNCTION};
                case KeywordStringConstants.HOLDQUEUE_ALT_SPELLING: case KeywordStringConstants.HOLDQUEUE:  return new TokenTypeType[]{TokenTypeType.FUNCTION};
                case KeywordStringConstants.RELEASE: return new TokenTypeType[]{TokenTypeType.FUNCTION};
                case KeywordStringConstants.WHILE: return new TokenTypeType[]{TokenTypeType.CTRL};
                case KeywordStringConstants.QUEUELENGTH_ALT_SPELLING: case KeywordStringConstants.QUEUELENGTH:  return new TokenTypeType[]{TokenTypeType.ATTRIBUTE};
                case KeywordStringConstants.MAXQUEUE_ALT_SPELLING: case KeywordStringConstants.MAXQUEUE:  return new TokenTypeType[]{TokenTypeType.PARAMETER};
                case KeywordStringConstants.MINQUEUE_ALT_SPELLING: case KeywordStringConstants.MINQUEUE:  return new TokenTypeType[]{TokenTypeType.PARAMETER};
                case KeywordStringConstants.MIN: return new TokenTypeType[]{TokenTypeType.FUNCTION};
                case KeywordStringConstants.MAX: return new TokenTypeType[]{TokenTypeType.FUNCTION};
                case KeywordStringConstants.NOT: return new TokenTypeType[]{TokenTypeType.CTRL_OPERATOR};
                case KeywordStringConstants.SHIFT: return new TokenTypeType[]{TokenTypeType.FUNCTION};
                case KeywordStringConstants.UNSHIFT: return new TokenTypeType[]{TokenTypeType.FUNCTION};
                case KeywordStringConstants.AND: return new TokenTypeType[]{TokenTypeType.CTRL_OPERATOR};
                case KeywordStringConstants.ROUND: return new TokenTypeType[]{TokenTypeType.FUNCTION};
                case KeywordStringConstants.PERMANENT: return new TokenTypeType[]{TokenTypeType.COMMAND};
                case KeywordStringConstants.OR: return new TokenTypeType[]{TokenTypeType.CTRL_OPERATOR};
                case KeywordStringConstants.GET: return new TokenTypeType[]{TokenTypeType.FUNCTION};
                case KeywordStringConstants.CASE: return new TokenTypeType[]{TokenTypeType.CTRL};
                case KeywordStringConstants.BREAK: return new TokenTypeType[]{TokenTypeType.COMMAND};
                case KeywordStringConstants.FUNCTION: return new TokenTypeType[]{TokenTypeType.VALUETYPE, TokenTypeType.NAMESPACETYPE};
                case KeywordStringConstants.COMMENT: return new TokenTypeType[]{TokenTypeType.PARAMETER};
                case KeywordStringConstants.MODIFY: return new TokenTypeType[]{TokenTypeType.COMMAND};
                case KeywordStringConstants.INT: return new TokenTypeType[]{TokenTypeType.VALUETYPE};
                case KeywordStringConstants.STRING: return new TokenTypeType[]{TokenTypeType.VALUETYPE};
                case KeywordStringConstants.COMPONENT: return new TokenTypeType[]{TokenTypeType.VALUETYPE};
                case KeywordStringConstants.ALIAS: return new TokenTypeType[]{TokenTypeType.VALUE};
                case KeywordStringConstants.DEPENDANCY: return new TokenTypeType[]{TokenTypeType.PARAMETER};
                case KeywordStringConstants.IF: return new TokenTypeType[]{TokenTypeType.CTRL};
                case KeywordStringConstants.FOR: return new TokenTypeType[]{TokenTypeType.COMMAND};
                case KeywordStringConstants.FOREACH: return new TokenTypeType[]{TokenTypeType.COMMAND};
                case KeywordStringConstants.IN: return new TokenTypeType[]{TokenTypeType.COMMAND};
                case KeywordStringConstants.QUEUE: return new TokenTypeType[]{TokenTypeType.ATTRIBUTE};
                case KeywordStringConstants.VOID: return new TokenTypeType[]{TokenTypeType.VALUETYPE};
                case KeywordStringConstants.NAMESPACE: return new TokenTypeType[]{TokenTypeType.VALUETYPE};
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
        VOID,
        NAMESPACE,
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
        public TokenTypeType[] TypesOfTType{ get; init; }
        public Token(TokenType aTokenType, TokenTypeType[] tokenTypeType, int aLine, string aLexeme, string? aLiteral){
            Line = aLine;
            Lexeme = aLexeme;
            Literal = aLiteral;
            Type = aTokenType;
            TypesOfTType = tokenTypeType;
        }
        public override string ToString(){
            return Lexeme;
        }
    }
}
