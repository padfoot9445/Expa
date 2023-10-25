namespace Tokens{
    using System.Collections.Generic;
    public static class Keywords{
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