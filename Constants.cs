namespace Constants{
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
        public const string GLOBAL_DEFAULT_DISPLAY = "global";
    }
    internal static class KeywordStringConstants{
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
    }
}