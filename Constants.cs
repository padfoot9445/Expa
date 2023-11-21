namespace Constants
{
    using Tokens;
    internal static class DBAccessConstants
    {
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
        public static class TableNames
        {
            public const string MAIN_TABLE_NAME = "Objects";
            public const string GLOBAL_TABLE_NAME = "Global";
            public const string NATION_TABLE_NAME = "Nation";
            public const string AREA_TABLE_NAME = "Area";
            public const string FUNCTION_TABLE_NAME = "Function";
        }
        public static class ColumnNames
        {
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
        public static class DBValues
        {
            public const string GLOBAL = "global";
            public const string NATION = "nation";
            public const string AREA = "area";
            public const string FUNCTION = "function";
            public const string TEMPLATE = "template";
        }
        public const char CSVDELIMITER = ',';
    }
}


