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
        protected readonly string filePath;
        protected SqliteConnection connection;
        #region Ordinals
        protected int TypeOrdinal{get; }
        protected int IdentifierOrdinal{get; }
        protected int ParentOrdinal{get; }
        protected int DisplayOrdinal{get; }
        protected int CommentOrdinal{get; }
        protected int IDOrdinal{get; }
        #endregion
        protected FileHandlerBase(string filePath){
            this.filePath = filePath;
            connection = new SqliteConnection($"Data Source={filePath}");
            connection.Open();
            using (var reader = ExecuteQuery(DBAccessConstants.GET_COLUMN_NAMES_QUERY))
            {
                TypeOrdinal = reader.GetOrdinal(DBAccessConstants.ColumnNames.TYPE); //string, 2
                IdentifierOrdinal = reader.GetOrdinal(DBAccessConstants.ColumnNames.IDENTIFIER);//string, 1
                ParentOrdinal = reader.GetOrdinal(DBAccessConstants.ColumnNames.PARENT);//string, 2
                DisplayOrdinal = reader.GetOrdinal(DBAccessConstants.ColumnNames.DISPLAY);//string, 3
                CommentOrdinal = reader.GetOrdinal(DBAccessConstants.ColumnNames.COMMENT);//string, 4
                IDOrdinal = reader.GetOrdinal(DBAccessConstants.ColumnNames.ID);//string, 5
            }
        }
        protected void Close() => connection.Close();
        protected void ExecuteNonQuery(string command){
            using(var myCommand = new SqliteCommand(command, connection)){
                myCommand.ExecuteNonQuery();
            }
        }
        protected SqliteDataReader ExecuteQuery(string command) => new SqliteCommand(command, connection).ExecuteReader();
    }

    internal class LoadObjects: FileHandlerBase{
        //store a csv of parent IDs rather than parent
        internal LoadObjects(string filePath): base(filePath){
            MakeTables();
        }
        private void MakeTables(){
            foreach(var command in DBAccessConstants.MAKE_TABLES_IF_NOT_EXISTS){
                ExecuteNonQuery(command);
            }

        }
        
        private bool DbExists(){
            if(File.Exists(filePath)){return true;} return false;
        }
    }
    internal class FileHandler: FileHandlerBase{
        private readonly LoadObjects loader;
        private readonly WriteObjects writer;
        internal FileHandler(string filePath): base(filePath){
            loader = new(filePath);
            writer = new(filePath);
        }
        internal BaseExpaObject GetObject(string identifier){
            Result TSearchResult = SearchObject(identifier);
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
            expaObject.ParentStringID = searchResult.ParentStringID!;//we know this is not global because we returned before
            return expaObject;
        }
        private Result SearchObject(string key)=>SearchObjects(key, DBAccessConstants.ColumnNames.IDENTIFIER)[0];
        private SqliteDataReader IDSearchTable(string id, string table) => RSearchTable(id, table, DBAccessConstants.ColumnNames.ID);
        private Result[] SearchObjects(string key, string column){
            //assume the caller knows the thing exists
            List<Result> rl = new();
            using(var reader = RSearchTable(key, DBAccessConstants.TableNames.MAIN_TABLE_NAME, column) ){
                while(reader.Read()){
                    SqliteDataReader paramReader = IDSearchTable(reader.GetString(IDOrdinal), reader.GetString(TypeOrdinal));
                    string objectTypeString = reader.GetString(TypeOrdinal);
                    string? objectParentID = reader.GetString(ParentOrdinal);
                    string identifier = reader.GetString(IdentifierOrdinal);
                    string display = reader.GetString(DisplayOrdinal);
                    string comment = reader.GetString(CommentOrdinal);
                    string id = reader.GetString(IDOrdinal);
                    BaseExpaObject expaObject = objectTypeString switch{
                        //if there is somehow an error here it has to be reported as a compiler error, not a user error
                        DBAccessConstants.DBValues.GLOBAL => new ExpaGlobal(
                            scope: Parser.UnparsedScopes(id, TokenType.GLOBAL),
                            time: BackgroundTime.ParseAcTime(paramReader[DBAccessConstants.ColumnNames.TIME].ToString()!),
                            display: display,
                            comment: comment
                        ),
                        DBAccessConstants.DBValues.NATION => new ExpaNation(
                            parentStringID: objectParentID!,
                            identifier: identifier,
                            scope: Parser.UnparsedScopes(id, TokenType.NATION),
                            time: BackgroundTime.ParseAcTime(paramReader[DBAccessConstants.ColumnNames.TIME].ToString()!),
                            MaxChildShipSize: (int)paramReader[DBAccessConstants.ColumnNames.MINCSS],
                            MinChildShipSize: (int)paramReader[DBAccessConstants.ColumnNames.MAXCSS], 
                            display: display, 
                            comment: comment
                        ),
                        DBAccessConstants.DBValues.AREA => new ExpaArea(
                            parentStringID: objectParentID, 
                            identifier: identifier,
                            aScope: Parser.UnparsedScopes(id, TokenType.AREA),
                            minCSS: (int)paramReader[DBAccessConstants.ColumnNames.MINCSS], 
                            maxCSS: (int)paramReader[DBAccessConstants.ColumnNames.MAXCSS], 
                            display: display, 
                            comment: comment
                        ),
                        DBAccessConstants.DBValues.FUNCTION => new ExpaFunction(
                            parentStringID: objectParentID, 
                            identifier: identifier,
                            scope: Parser.UnparsedScopes(id, TokenType.FUNCTION),
                            display: display,
                            comment: comment
                        ),//TODO: Add support for template
                        _ => throw new MainException("error while parsing db file")
                        //TODO: Add loading support for various objects
                    };
                    rl.Add(new Result(expaObject, objectParentID, reader[DBAccessConstants.ColumnNames.CHILDREN].ToString()!.Split(Constants.DBAccessConstants.CSVDELIMITER)/*children*/));
                }
            }
            return rl.ToArray();
        }
        private SqliteDataReader RSearchTable(string key, string table, string column){
                var command = connection.CreateCommand();
                command.CommandText = 
                @$"SELECT * FROM {table} WHERE {column} = {key}
                ";
                return command.ExecuteReader();
            
        }
        // public HashSet<string> GetAllIdentifiers(){
        //     using(var connection = new SqliteConnection($"Data Source={filePath}")){
        //         connection.Open();
        //         var command = connection.CreateCommand();
        //         command.CommandText = 
        //         @$"SELECT identifier FROM Objects";
        //         HashSet<string> rv = new();
        //         using(var reader = command.ExecuteReader()){
        //             while(reader.Read()){
        //                 rv.Add(reader.GetString(0));
        //             }
        //         }
        //         return rv;
        //     }
        // }
        internal bool ItemExists(string key, string table, string column) => RSearchTable(key, table, column).HasRows;
    }
    
    internal class WriteObjects: FileHandlerBase{
        internal WriteObjects(string filePath): base(filePath){

        }
    }
}