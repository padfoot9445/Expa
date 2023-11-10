namespace FileHandler{
    using Microsoft.Data.Sqlite;
    using ExpaObjects;
    using Structs;
    using BackgroundObjects;
    using Errors;
    using Parser;
    public abstract class FileHandlerBase{
        public readonly string filePath;
        public SqliteConnection connection;
        public FileHandlerBase(string filePath){
            this.filePath = filePath;
            connection = new SqliteConnection($"Data Source={filePath}");
            connection.Open();
        }
        public void Close() => connection.Close();
    }

    public class LoadObjects: FileHandlerBase{
        //store a csv of parent IDs rather than parent
        public LoadObjects(string filePath): base(filePath){
            MakeTables();
        }
        private void MakeTables(){
            string[] commands = {
                "CREATE TABLE IF NOT EXISTS Objects (identifier string,  type string,  parents string, display string,  comment string,  children string)",
                "CREATE TABLE IF NOT EXISTS Global (identifier string, time int)",
                "CREATE TABLE IF NOT EXISTS Nation (identifier string, time int, minChildShipSize int, maxChildShipSize int)",
                "CREATE TABLE IF NOT EXISTS Area (identifier string, minChildShipSize int, maxChildShipSize int)"
            };
            foreach(var command in commands){
                ExecuteNonQuery(command);
            }

        }
        private void ExecuteNonQuery(string command){
            using(var myCommand = new SqliteCommand(command, connection)){
                myCommand.ExecuteNonQuery();
            }
        }
        
        private bool DbExists(){
            if(File.Exists(filePath)){return true;} return false;
        }
    }
    public class FileHandler: FileHandlerBase{


        public readonly LoadObjects loader;
        public readonly WriteObjects writer;
        public FileHandler(string filePath): base(filePath){
            loader = new(filePath);

            writer = new(filePath);
        }
        private readonly Dictionary<string, ExpaObject> cachedObjects = new();
        public ExpaObject GetObject(string identifier){
            Result searchResult = SearchDB(identifier);
            ExpaObject AddParents(ExpaObject expaObject){
                if(searchResult.parentIdentifiers[0] != ""){
                    foreach(string parentIdentifier in searchResult.parentIdentifiers){
                        expaObject.AddParent(parentIdentifier);
                        if(!cachedObjects.ContainsKey(parentIdentifier)){cachedObjects[parentIdentifier] = GetObject(parentIdentifier);}
                    }
                }
                return expaObject;
            }

            if(searchResult.childIdentifiers != null && searchResult.childIdentifiers[0] != ""){
                ExpaNameSpace expaObject = (ExpaNameSpace)searchResult.expaObject;
                foreach(string childIdentifier in searchResult.childIdentifiers){
                    expaObject.AddChild(childIdentifier);
                    if(!cachedObjects.ContainsKey(childIdentifier)){cachedObjects[childIdentifier] = GetObject(childIdentifier);}
                }
                return AddParents(expaObject);
            } else{
                return AddParents(searchResult.expaObject);
            }
        }
        public Result SearchDB(string key)=>SearchTable(key, "Objects");
        public Result SearchTable(string key, string table)=>CSearchTable(key, table, "identifier")[0];
        public SqliteDataReader IdentifierSearchTable(string id, string table) => RSearchTable(id, table, "identifier");
        public Result[] CSearchTable(string key, string table, string column){
            //assume the caller knows the thing exists
            List<Result> rl = new();
            using(var reader = RSearchTable(key, table, column) ){
                int TYPEORDINAL = reader.GetOrdinal("type"); //string, 2
                int IDENTIFIERORDINAL = reader.GetOrdinal("identifier");//string, 1
                int PARENTSORDINAL = reader.GetOrdinal("parents");//string, csv, 2
                int DISPLAYORDINAL = reader.GetOrdinal("display");//string, 3
                int COMMENTORDINAL = reader.GetOrdinal("comment");//string, 4
                while(reader.Read()){
                    string[] parents = reader.GetString(PARENTSORDINAL).Split(',');
                    SqliteDataReader paramReader = IdentifierSearchTable(reader.GetString(IDENTIFIERORDINAL), reader.GetString(TYPEORDINAL));
                    ExpaObject expaObject = reader.GetString(TYPEORDINAL) switch{
                        //if there is somehow an error here it has to be reported as a compiler error, not a user error
                        "global" => new ExpaGlobal(Parser.unparsedScopes[reader.GetString(IDENTIFIERORDINAL)], Time.ParseAcTime(paramReader["time"].ToString()!), reader.GetString(DISPLAYORDINAL), reader.GetString(COMMENTORDINAL)),
                        "nation" => new ExpaNation((ICanBeParent<ExpaNation>)GetObject(parents[0]), Parser.unparsedScopes[reader.GetString(IDENTIFIERORDINAL)],(Time)paramReader["time"],(int)paramReader["minChildShipSize"], (int)paramReader["maxChildShipSize"], reader.GetString(DISPLAYORDINAL), reader.GetString(COMMENTORDINAL)),
                        "area" => new ExpaArea((ICanBeParent<ExpaArea>)GetObject(parents[0]), (ExpaNation)GetObject(paramReader["nationParent"].ToString()!), (int)paramReader["minChildShipSize"], (int)paramReader["maxChildShipSize"], Parser.unparsedScopes[reader.GetString(IDENTIFIERORDINAL)], reader.GetString(DISPLAYORDINAL), reader.GetString(COMMENTORDINAL)), 
                        _ => throw new MainException("error while parsing db file")
                    };
                    rl.Add(new Result(expaObject, parents,reader["children"].ToString()!.Split(',')/*children*/));
                }
            }
            return rl.ToArray();
        }
        public SqliteDataReader RSearchTable(string key, string table, string column){
                var command = connection.CreateCommand();
                command.CommandText = 
                @$"SELECT * FROM {table} WHERE {column} = {key}
                ";
                return command.ExecuteReader();
            
        }
        public HashSet<string> GetAllIdentifiers(){
            using(var connection = new SqliteConnection($"Data Source={filePath}")){
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = 
                @$"SELECT identifier FROM Objects";
                HashSet<string> rv = new();
                using(var reader = command.ExecuteReader()){
                    while(reader.Read()){
                        rv.Add(reader.GetString(0));
                    }
                }
                return rv;
            }
        }
        public bool ItemExists(string key, string table, string column) => RSearchTable(key, table, column).HasRows;
    }
    
    public class WriteObjects: FileHandlerBase{
        public WriteObjects(string filePath): base(filePath){

        }
    }
}

namespace StorageObjects{
    using ExpaObjects;
    using System.Collections;
    static class Main{
        public static Dictionary<string, ExpaStorageObject> ObjFromClassName = new();
    }
    public class ExpaStorageObject{}
}