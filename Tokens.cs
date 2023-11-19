namespace Tokens
{
    using Constants;
    internal static class Keywords{
        public static TokenType KeyWords(string input){
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
                case KeywordStringConstants.PROGRAM: return TokenType.PROGRAM;
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
                case KeywordStringConstants.MAXQUEUE_ALT_SPELLING: case KeywordStringConstants.MAXQUEUE:  return TokenType.MAXQUEUE;//idk what this does; i'm going to assume its an attribute: but it can be replaced with max anyways
                case KeywordStringConstants.MINQUEUE_ALT_SPELLING: case KeywordStringConstants.MINQUEUE:  return TokenType.MINQUEUE;
                case KeywordStringConstants.MIN: return TokenType.MIN;
                case KeywordStringConstants.MAX: return TokenType.MAX;
                case KeywordStringConstants.NOT: return TokenType.NOT;
                case KeywordStringConstants.SHIFT: return TokenType.SHIFT;
                case KeywordStringConstants.UNSHIFT: return TokenType.UNSHIFT;
                case KeywordStringConstants.AND: return TokenType.AND;
                case KeywordStringConstants.REPEAT: return TokenType.REPEAT;
                case KeywordStringConstants.ROUND: return TokenType.ROUND;
                case KeywordStringConstants.PERMANENT: return TokenType.PERMANENT;
                case KeywordStringConstants.OR: return TokenType.OR;
                case KeywordStringConstants.GET: return TokenType.GET;
                case KeywordStringConstants.CASE: return TokenType.CASE;
                case KeywordStringConstants.BREAK: return TokenType.BREAK;
                case KeywordStringConstants.FUNCTION: return TokenType.FUNCTION;
                case KeywordStringConstants.COMMENT: return TokenType.COMMENT;
                case KeywordStringConstants.MODIFY: return TokenType.MODIFY;
                case KeywordStringConstants.IPARENT: return TokenType.IPARENT;
                case KeywordStringConstants.INT: return TokenType.INT;
                case KeywordStringConstants.STRING: return TokenType.STRING;
                case KeywordStringConstants.COMPONENT: return TokenType.COMPONENT;
                case KeywordStringConstants.ALIAS: return TokenType.ALIAS;
                case KeywordStringConstants.DEPENDANCY: return TokenType.DEPENDANCY;
                default: return TokenType.INTERPRETERNULL;

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
        //Interpreter only Tokens:
        INTERPRETERNULL,
        ONE
    }
    //implement arguments, round(true, false);
    internal class Token{
        public TokenType tokenType;
        public int line;
        public string lexeme;
        public string? literal;
        public Token(TokenType aTokenType, int aLine, string aLexeme, string? aLiteral){
            line = aLine;
            lexeme = aLexeme;
            literal = aLiteral;
            tokenType = aTokenType;
        }
        public override string ToString(){
            return lexeme;
        }
    }
}