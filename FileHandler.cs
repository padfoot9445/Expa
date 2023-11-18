namespace FileHandler{
    using Microsoft.Data.Sqlite;
    using ExpaObjects;
    using Structs;
    using BackgroundObjects;
    using Errors;
    using Markers;
    using Tokens;
    using Interfaces;
    using Constants;
    using Parser;
    internal abstract class FileHandlerBase{
        public readonly string filePath;
        public SqliteConnection connection;
        public FileHandlerBase(string filePath){
            this.filePath = filePath;
            connection = new SqliteConnection($"Data Source={filePath}");
            connection.Open();
        }
        public void Close() => connection.Close();
    }

    internal class LoadObjects: FileHandlerBase{
        //store a csv of parent IDs rather than parent
        public LoadObjects(string filePath): base(filePath){
            MakeTables();
        }
        private void MakeTables(){
            foreach(var command in DBAccessConstants.MAKE_TABLES_IF_NOT_EXISTS){
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
    internal class FileHandler: FileHandlerBase{


        public readonly LoadObjects loader;
        public readonly WriteObjects writer;
        public FileHandler(string filePath): base(filePath){
            loader = new(filePath);

            writer = new(filePath);
        }
        public BaseExpaObject GetObject(string identifier){
            Result TSearchResult = SearchDB(identifier);
            Structs.Result searchResult = (Structs.Result)TSearchResult;
            BaseExpaNonGlobalObject expaObject;
            if(searchResult.ExpaObject.IsNameSpace){
                BaseExpaNameSpace expaNameSpace = (BaseExpaNameSpace)searchResult.ExpaObject;
                foreach(string childIdentifier in searchResult.ChildStringIDs){
                    expaNameSpace.ChildrenStringIDs.Add(childIdentifier);
                }
                if(searchResult.ExpaObject.Type == TokenType.GLOBAL){
                    return expaNameSpace;
                }
                expaObject = expaNameSpace;
            } else{
                expaObject = (BaseExpaNonGlobalObject)searchResult.ExpaObject;
            }
            expaObject.ParentStringID = searchResult.ParentStringID;//we know this is not global because we returned before
            return expaObject;
        }
        private Result SearchDB(string key)=>SearchTable(key, "Objects");
        private Result SearchTable(string key, string table)=>CSearchTable(key, table, "identifier")[0];
        public SqliteDataReader IdentifierSearchTable(string id, string table) => RSearchTable(id, table, "identifier");
        //TODO: Refactor all hardcoded values to Constants.cs
        public Result[] CSearchTable(string key, string table, string column){
            //assume the caller knows the thing exists
            List<Result> rl = new();
            using(var reader = RSearchTable(key, table, column) ){
                int TYPE_ORDINAL = reader.GetOrdinal("type"); //string, 2
                int IDENTIFIER_ORDINAL = reader.GetOrdinal("identifier");//string, 1
                int PARENT_ORDINAL = reader.GetOrdinal("parent");//string, 2
                int DISPLAY_ORDINAL = reader.GetOrdinal("display");//string, 3
                int COMMENT_ORDINAL = reader.GetOrdinal("comment");//string, 4
                int ID_ORDINAL = reader.GetOrdinal("id");//string, 5

                while(reader.Read()){
                    SqliteDataReader paramReader = IdentifierSearchTable(reader.GetString(IDENTIFIER_ORDINAL), reader.GetString(TYPE_ORDINAL));
                    string objectTypeString = reader.GetString(TYPE_ORDINAL);
                    string? objectParentID = reader.GetString(PARENT_ORDINAL);
                    string identifier = reader.GetString(IDENTIFIER_ORDINAL);
                    string display = reader.GetString(DISPLAY_ORDINAL);
                    string comment = reader.GetString(COMMENT_ORDINAL);
                    string id = reader.GetString(ID_ORDINAL);
                    BaseExpaObject expaObject = objectTypeString switch{
                        //if there is somehow an error here it has to be reported as a compiler error, not a user error
                        "global" => new ExpaGlobal(
                            scope: Parser.UnparsedScopes(id, TokenType.GLOBAL),
                            time: BackgroundTime.ParseAcTime(paramReader["time"].ToString()!),
                            display: display,
                            comment: comment
                        ),
                        "nation" => new ExpaNation(
                            parentStringID: objectParentID!,
                            identifier: identifier,
                            scope: Parser.UnparsedScopes(id, TokenType.NATION),
                            time: BackgroundTime.ParseAcTime(paramReader["time"].ToString()!),
                            MaxChildShipSize: (int)paramReader["minChildShipSize"],
                            MinChildShipSize: (int)paramReader["maxChildShipSize"], 
                            display: display, 
                            comment: comment
                        ),
                        "area" => new ExpaArea(
                            parentStringID: objectParentID, 
                            identifier: identifier,
                            aScope: Parser.UnparsedScopes(id, TokenType.AREA),
                            minCSS: (int)paramReader["minChildShipSize"], 
                            maxCSS: (int)paramReader["maxChildShipSize"], 
                            display: display, 
                            comment: comment
                        ),
                        "function" => new ExpaFunction(
                            parentStringID: objectParentID, 
                            identifier: identifier,
                            scope: Parser.UnparsedScopes(id, TokenType.FUNCTION),
                            display: display,
                            comment: comment
                            ),
                        _ => throw new MainException("error while parsing db file")
                        //TODO: Add loading support for various objects
                    };
                    rl.Add(new Result(expaObject, objectParentID, reader["children"].ToString()!.Split(',')/*children*/));
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
    
    internal class WriteObjects: FileHandlerBase{
        public WriteObjects(string filePath): base(filePath){

        }
    }
}

namespace StorageObjects
{
    static class Main{
        public static Dictionary<string, ExpaStorageObject> ObjFromClassName = new();
    }
    public class ExpaStorageObject{}
}