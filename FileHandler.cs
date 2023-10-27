namespace FileHandler{
    using Microsoft.Data.Sqlite;
    using System.Data;
    using System;
    using ExpaObjects;
    using Newtonsoft.Json;
    using System.Net.Security;
    using Helpers;

    public class FileHandler{
        public readonly string filePath;
        public FileHandler(string filePath){
            this.filePath = filePath;
        }
        private bool DbExists(){
            if(File.Exists(filePath)){return true;} return false;
        }
        public readonly Dictionary<String,Func<
        public Result[] SearchDB(string key)=>SearchTable(key, "Objects");
        public Result[] SearchTable(string key, string table)=>CSearchTable(key, table, "identifier");
        public Result[] CSearchTable(string key, string table, string column){
            using(var connection = new SqliteConnection($"Data Source={filePath}")){
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = 
                @$"FROM {table}
                SELECT *
                WHERE {column} = {key}
                ";
                List<Result> rl = new();
                using(var reader = command.ExecuteReader()){
                    while(reader.Read()){
                        rl.Add(
                            new Result(
                                Activator.CreateInstance(Converters.StringToType((string)reader["type"]))
                            )
                        )
                    }
                }
            }
        }
    }
    
    public readonly struct Result{
        public readonly ExpaObject expaObject;
        public readonly string[] parentIdentifiers;
        public readonly string[] childIdentifiers;
        public Result(ExpaObject expaObject, string[] parentIdentifiers, string[] childIdentifiers){
            this.expaObject = expaObject;
            this.parentIdentifiers = parentIdentifiers;
            this.childIdentifiers = childIdentifiers;
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