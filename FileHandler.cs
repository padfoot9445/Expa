namespace FileHandler{
    using Microsoft.Data.Sqlite;
    using ExpaObjects;
    using Structs;
    using BackgroundObjects;
    using Errors;

    public class FileHandler{
        //store a csv of parent IDs rather than parent
        public readonly string filePath;
        public readonly Dictionary<string, Scope> unparsedScopes;
        SqliteConnection connection;
        public FileHandler(string filePath, Dictionary<string, Scope> unparsedScopes){
            this.filePath = filePath;
            this.unparsedScopes = unparsedScopes;
            connection = new SqliteConnection($"Data Source={filePath}");
        }
        
        private bool DbExists(){
            if(File.Exists(filePath)){return true;} return false;
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
                int TYPEORDINAL = reader.GetOrdinal("type");
                int IDENTIFIERORDINAL = reader.GetOrdinal("identifier");
                int PARENTSORDINAL = reader.GetOrdinal("parents");
                int DISPLAYORDINAL = reader.GetOrdinal("display");
                int COMMENTORDINAL = reader.GetOrdinal("comment");
                while(reader.Read()){
                    string[] parents = reader.GetString(PARENTSORDINAL).Split(',');
                    SqliteDataReader paramReader = IdentifierSearchTable(reader.GetString(IDENTIFIERORDINAL)/*second entry in a object table will always be id*/, reader.GetString(TYPEORDINAL)/*third entry in a object table will always be type*/);
                    ExpaObject expaObject = reader.GetString(TYPEORDINAL) switch{
                        "global" => new ExpaGlobal(unparsedScopes[reader.GetString(IDENTIFIERORDINAL)], Time.ParseAcTime(paramReader["time"].ToString()!)),
                        "nation" => new ExpaNation((ExpaNameSpace)GetObject(parents[0]), unparsedScopes[reader.GetString(IDENTIFIERORDINAL)],(Time)paramReader["time"], reader.GetString(DISPLAYORDINAL), reader.GetString(COMMENTORDINAL)),
                        _ => throw new MainException("error while parsing db file")
                    };
                    rl.Add(new Result(expaObject, parents,reader["children"].ToString()!.Split(',')/*children*/));
                }
            }
            return rl.ToArray();
        }
        public SqliteDataReader RSearchTable(string key, string table, string column){
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = 
                @$"FROM {table}
                SELECT *
                WHERE {column} = {key}
                ";
                return command.ExecuteReader();
            
        }
        public string[] GetAllIdentifiers(){
            using(var connection = new SqliteConnection($"Data Source={filePath}")){
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = 
                @$"FROM objects
                SELECT identifier
                ";
                List<string> rv = new();
                using(var reader = command.ExecuteReader()){
                    while(reader.Read()){
                        rv.Add(reader.GetString(0));
                    }
                }
                return rv.ToArray();
            }
        }
        public bool ItemExists(string key, string table, string column) => RSearchTable(key, table, column).HasRows;
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