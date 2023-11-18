namespace Constants{
    internal static class DBAccessConstants{
        public static readonly string[] MAKE_TABLES_IF_NOT_EXISTS = {
                "CREATE TABLE IF NOT EXISTS Objects (identifier string,  type string,  parents string, display string,  comment string,  children string)",
                "CREATE TABLE IF NOT EXISTS Global (identifier string, time int)",
                "CREATE TABLE IF NOT EXISTS Nation (identifier string, time int, minChildShipSize int, maxChildShipSize int)",
                "CREATE TABLE IF NOT EXISTS Area (identifier string, minChildShipSize int, maxChildShipSize int)",
                "CREATE TABLE IF NOT EXISTS Funciton (identifier string, args string, returnType string)"//args: type1 name1, type2 name2...
            };
        public const string DBSTRING_GLOBAL = "global";
    }
    internal static class ExpaObjectConstants{
        public const string OBJECTIDSEPERATOR = ".";
    }
}