namespace Constants{
    using Tokens;
    internal static class DBAccessConstants{
        public static readonly string[] MAKE_TABLES_IF_NOT_EXISTS = {
                $"CREATE TABLE IF NOT EXISTS {TableNames.MAIN_TABLE_NAME} ({ColumnNames.ID} string, {ColumnNames.IDENTIFIER} string, {ColumnNames.TYPE} string,  {ColumnNames.PARENT} string, {ColumnNames.DISPLAY} string,  {ColumnNames.COMMENT} string, {ColumnNames.CHILDREN} string)",
                $"CREATE TABLE IF NOT EXISTS {TableNames.GLOBAL_TABLE_NAME} ({ColumnNames.ID} string, {ColumnNames.TIME} int)",
                $"CREATE TABLE IF NOT EXISTS {TableNames.NATION_TABLE_NAME} ({ColumnNames.ID} string, {ColumnNames.TIME} int, {ColumnNames.MINCSS} int, {ColumnNames.MAXCSS} int)",
                $"CREATE TABLE IF NOT EXISTS {TableNames.AREA_TABLE_NAME} ({ColumnNames.ID} string, {ColumnNames.MINCSS} int, {ColumnNames.MAXCSS} int)",
                $"CREATE TABLE IF NOT EXISTS {TableNames.FUNCTION_TABLE_NAME} ({ColumnNames.ID} string, {ColumnNames.FUNCTION_ARGUMENTS} string, {ColumnNames.RETURN_TYPE} string)"//args: type1 name1, type2 name2...
            };
        public static readonly string GET_COLUMN_NAMES_QUERY = @$"SELECT COLUMN_NAME 
            FROM INFORMATION_SCHEMA.COLUMNS
            WHERE TABLE_NAME = {TableNames.MAIN_TABLE_NAME} ";
        public static class TableNames{
            public const string MAIN_TABLE_NAME = "Objects";
            public const string GLOBAL_TABLE_NAME = "Global";
            public const string NATION_TABLE_NAME = "Nation";
            public const string AREA_TABLE_NAME = "Area";
            public const string FUNCTION_TABLE_NAME = "Function";
        }
        public static class ColumnNames{
            public const string ID = "id";
            public const string IDENTIFIER = "identifier";
            public const string TYPE = "type";
            public const string PARENT = "parent";
            public const string DISPLAY = "display";
            public const string COMMENT = "comment";
            public const string CHILDREN = "children";
            public const string TIME = "time";
            public const string MINCSS = "minChildShipSize";
            public const string MAXCSS = "maxChildCSS";
            public const string FUNCTION_ARGUMENTS = "args";
            public const string RETURN_TYPE = "returnType";
        }
        public static class DBValues{
            public const string GLOBAL = "global";
            public const string NATION = "nation";
            public const string AREA = "area";
            public const string FUNCTION = "function";
            public const string TEMPLATE = "template";
        }
        public const char CSVDELIMITER = ',';
    }
    internal static class ExpaObjectConstants{
        public const string OBJECT_ID_SEPERATOR = ".";
        public const string GLOBAL_IDENTIFIER = "global";
        public const string GLOBAL_DEFAULT_DISPLAY = GLOBAL_IDENTIFIER;
    }
    internal static class KwStrConsts{
        public const string GLOBAL = "global";
        public const string NEW = "new";
        public const string TIME = "time";
        public const string NATION = "nation";
        public const string SPEED = "speed";
        public const string DISPLAY = "display";
        public const string AREA = "area";
        public const string VIEW = "view";
        public const string INFORMATION = "information";
        public const string PROGRAM = "program";
        public const string ALL = "all";
        public const string USING = "using";
        public const string FROM = "from";
        public const string SHIPYARD = "shipyard";
        public const string BERTHS = "berths";
        public const string MAXSIZE = "maxSize";
        public const string MINSIZE = "minSize";
        public const string SHIPCLASS = "shipclass";
        public const string TEMPLATE = "template";
        public const string TRUE = "true";
        public const string FALSE = "false";
        public const string EQUALIZE = "equalize";
        public const string SWITCH = "switch";
        public const string ELSE = "else";
        public const string ADD = "add";
        public const string REMOVE = "remove";
        public const string HOLDQUEUE = "holdqueue";
        public const string RELEASE = "release";
        public const string WHILE = "while";
        public const string QUEUELENGTH = "queuelength";
        public const string MAXQUEUE = "maxqueue";
        public const string MINQUEUE = "minqueue";
        public const string MIN = "min";
        public const string MAX = "max";
        public const string NOT = "not";
        public const string SHIFT = "shift";
        public const string UNSHIFT = "unshift";
        public const string AND = "and";
        public const string REPEAT = "repeat";
        public const string ROUND = "round";
        public const string PERMANENT = "permanent";
        public const string OR = "or";
        public const string GET = "get";
        public const string CASE = "case";
        public const string BREAK = "break";
        public const string FUNCTION = "function";
        public const string COMMENT = "comment";
        public const string MODIFY = "modify";
        public const string IPARENT = "iParent";
        public const string INT = "int";
        public const string STRING = "string";
        public const string COMPONENT = "component";
        public const string ALIAS = "alias";
        public const string DEPENDANCY = "dependancy";
        public const string MAXSIZE_ALT_SPELLING = "maxsize"; 
        public const string MINSIZE_ALT_SPELLING = "minsize"; 
        public const string HOLDQUEUE_ALT_SPELLING = "holdQueue"; 
        public const string QUEUELENGTH_ALT_SPELLING = "queueLength"; 
        public const string MAXQUEUE_ALT_SPELLING = "maxQueue"; 
        public const string MINQUEUE_ALT_SPELLING = "minQueue"; 
        public const string OBJECT_TIME = "objectTime";
        public const string IF = "if";
        public const string FOR = "for";
        public const string FOREACH = "foreach";
        public const string IN = "in";
        public const string QUEUE = "queue";
        public const string VOID = "void";
        public const string NAMESPACE = "namespace";
        public const string LENGTH = "length";
        public const string TYPE = "type";
    }
    internal static class LexerConstants{
        #region literals
        public static class Literals{
            public const string LEFTSQUAREBRACKET = "[";
            public const string RIGHTSQUAREBRACKET = "[";
            public const string LEFTPAREN = "(";
            public const string RIGHTPAREN = ")";
            public const string LEFTBRACE = "{";
            public const string RIGHTBRACE = "}";
            public const string SEMICOLON = ";";
            public const string DOT = ".";
            public const string COLON = ":";
            public const string PLUS = "+";
            public const string MINUS = "-";
            public const string STAR = "*";
            public const string SLASH = "/";
            public const string PERCENT = "%";
            public const string DOUBLEEQUALS = "==";
            public const string EQUALS = "=";
            public const string COMMA = ",";
            
            public const string DOUBLE_MINUS = "--";
            public const string DOUBLE_PLUS = "++";
            public const string DOUBLE_STAR = "**";
        }
        public static class Chars{
            #region constant chars
            public const char LEFTSQUAREBRACKET = '[';
            public const char RIGHTSQUAREBRACKET = ']';
            public const char LEFTPAREN = '(';
            public const char RIGHTPAREN = ')';
            public const char LEFTBRACE = '{';
            public const char RIGHTBRACE = '}';
            public const char SEMICOLON = ';';
            public const char DOT = '.';
            public const char COLON = ':';
            public const char PLUS = '+';
            public const char MINUS = '-';
            public const char STAR = '*';
            public const char PERCENT = '%';
            public const char SPACE = ' ';
            public const char RETURN = '\r';
            public const char TAB = '\t';
            public const char NEWLINE = '\n';
            public const char SLASH = '/';
            public const char DOUBLE_QUOTE = '"';
            public const char EQUAL_SIGN = '=';
            public const char COMMA = ',';
            public const char UNDERSCORE = '_';
            #endregion
        }
        #endregion
        
        
    }
}