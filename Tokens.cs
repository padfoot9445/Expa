namespace Tokens{
    using System.Collections.Generic;
    public static class Keywords{
        public static TokenType keywords(string input){
            switch(input){
                case "global": return TokenType.GLOBAL;
                case "new": return TokenType.NEW;
                case "time": return TokenType.TIME;
                case "nation": return TokenType.NATION;
                case "speed": return TokenType.SPEED;
                case "display": return TokenType.DISPLAY;
                case "area": return TokenType.AREA;
                case "view": return TokenType.VIEW;
                case "information": return TokenType.INFORMATION;
                case "program": return TokenType.PROGRAM;
                case "all": return TokenType.ALL;
                case "using": return TokenType.USING;
                case "from": return TokenType.FROM;
                case "shipyard": return TokenType.SHIPYARD;
                case "berths": return TokenType.BERTHS;
                case "maxsize": case "maxSize": return TokenType.MAXSIZE;
                case "minsize": case "minSize": return TokenType.MINSIZE;
                case "ship": return TokenType.SHIP;
                case "template": return TokenType.TEMPLATE;
                case "true": return TokenType.TRUE;
                case "false": return TokenType.FALSE;
                case "equalize": return TokenType.EQUALIZE;
                case "switch": return TokenType.SWITCH;//replaces if
                case "else": return TokenType.ELSE;
                case "add": return TokenType.ADD;
                case "remove": return TokenType.REMOVE;
                case "holdQueue": case "holdqueue": return TokenType.HOLDQUEUE;
                case "release": return TokenType.RELEASE;
                case "while": return TokenType.WHILE;
                case "queueLength": case "queuelength": return TokenType.QUEUELENGTH;
                case "maxQueue": case "maxqueue": return TokenType.MAXQUEUE;//idk what this does; i'm going to assume its an attribute: but it can be replaced with max anyways
                case "minQueue": case "minqueue": return TokenType.MINQUEUE;
                case "min": return TokenType.MIN;
                case "max": return TokenType.MAX;
                case "not": return TokenType.NOT;
                case "shift": return TokenType.SHIFT;
                case "unshift": return TokenType.UNSHIFT;
                case "and": return TokenType.AND;
                case "repeat": return TokenType.REPEAT;
                case "round": return TokenType.ROUND;
                case "permanent": return TokenType.PERMANENT;
                case "or": return TokenType.OR;
                case "get": return TokenType.GET;
                case "case": return TokenType.CASE;
                case "break": return TokenType.BREAK;
                case "function": return TokenType.FUNCTION;
                case "comment": return TokenType.COMMENT;
                case "modify": return TokenType.MODIFY;
                case "iParent": return TokenType.IPARENT;
                case "int": return TokenType.INT;
                case "string": return TokenType.STRING;
                case "component": return TokenType.COMPONENT;
                case "alias": return TokenType.ALIAS;
                case "dependancy": return TokenType.DEPENDANCY;
                default: return TokenType.INTERPRETERNULL;

            }
        }
        public static bool IsValidArgumentName(TokenType input){
            switch(input){
                case TokenType.NATION:
                case TokenType.SPEED:
                case TokenType.TIME:
                case TokenType.DISPLAY:
                case TokenType.BERTHS:
                case TokenType.MAXSIZE:
                case TokenType.MINSIZE:
                case TokenType.EQUALIZE:
                case TokenType.MAX:
                case TokenType.COMMENT:
                    return true;
                default:
                    return false;
            }
        }
    }
    public enum TokenType{
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
        SHIP,
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
    public class Token{
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