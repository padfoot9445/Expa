namespace Tokens{
    using System.Collections.Generic;
    public static class Keywords{
        public TokenType keywords(string input){
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
                case "maxSize": return TokenType.MAXSIZE;
                case "minSize": return TokenType.MINSIZE;
                case "ship": return TokenType.SHIP;
                case "template": return TokenType.TEMPLATE;
                case "true": return TokenType.TRUE;
                case "false": return TokenType.FALSE;
                case "equalize": return TokenType.EQUALIZE;
                case "switch": return TokenType.SWITCH;//replaces if
                case "else": return TokenType.ELSE;
                case "add": return TokenType.ADD;
                case "remove": return TokenType.REMOVE;
                case "holdQueue": return TokenType.HOLDQUEUE;
                case "release": return TokenType.RELEASE;
                case "while": return TokenType.WHILE;
                case "queueLength": return TokenType.QUEUELENGTH;
                case "maxQueue": return TokenType.MAXQUEUE;//idk what this does; i'm going to assume its an attribute: but it can be replaced with max anyways
                case "minQueue": return TokenType.MINQUEUE;
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
                default: throw new KeyNotFoundException();

            }
        }
        public static readonly Dictionary<string, TokenType> keywords = new Dictionary<string, TokenType>(){
            {"global", TokenType.GLOBAL},
            {"new", TokenType.NEW},
            {"time", TokenType.TIME},
            {"nation", TokenType.NATION},
            {"speed", TokenType.SPEED},
            {"display", TokenType.DISPLAY},
            {"area", TokenType.AREA},
            {"view", TokenType.VIEW},
            {"information", TokenType.INFORMATION},
            {"program", TokenType.PROGRAM},
            {"all", TokenType.ALL},
            {"using", TokenType.USING},
            {"from", TokenType.FROM},
            {"shipyard", TokenType.SHIPYARD},
            {"berths", TokenType.BERTHS},
            {"maxSize", TokenType.MAXSIZE},
            {"minSize", TokenType.MINSIZE},
            {"ship", TokenType.SHIP},
            {"template", TokenType.TEMPLATE},
            {"true", TokenType.TRUE},
            {"false", TokenType.FALSE},
            {"equalize", TokenType.EQUALIZE},
            {"switch", TokenType.SWITCH},//replaces if
            {"else", TokenType.ELSE},
            {"add", TokenType.ADD},
            {"remove", TokenType.REMOVE},
            {"holdQueue", TokenType.HOLDQUEUE},
            {"release", TokenType.RELEASE},
            {"while", TokenType.WHILE},
            {"queueLength", TokenType.QUEUELENGTH},
            {"maxQueue", TokenType.MAXQUEUE},//idk what this does; i'm going to assume its an attribute, but it can be replaced with max anyways
            {"minQueue", TokenType.MINQUEUE},
            {"min", TokenType.MIN},
            {"max", TokenType.MAX},
            {"not", TokenType.NOT},
            {"shift", TokenType.SHIFT},
            {"unshift", TokenType.UNSHIFT},
            {"and", TokenType.AND},
            {"repeat", TokenType.REPEAT},
            {"round", TokenType.ROUND},
            {"permanent", TokenType.PERMANENT},
            {"or", TokenType.OR},
            {"get", TokenType.GET},
            {"case", TokenType.CASE},
            {"break", TokenType.BREAK},
            {"function", TokenType.FUNCTION},
            {"comment", TokenType.COMMENT},
            {"modify", TokenType.MODIFY}
        };
        public static readonly HashSet<TokenType> argumentNames = new(){
            TokenType.TIME,
            TokenType.NATION,
            TokenType.SPEED,
            TokenType.DISPLAY,
            TokenType.BERTHS,
            TokenType.MAXSIZE,
            TokenType.MINSIZE,
            TokenType.EQUALIZE,
            TokenType.MAX,
            TokenType.COMMENT
            };
    }
    public enum TokenType{
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
        MODIFY
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