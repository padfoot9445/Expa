namespace interpreter
{

    using lexer;
    using Helpers;
    using Errors;
    using System.IO;
    using Parser;
    using ExpaObjects;

    public class Interpreter{
        private static (string, int, int, int) GetShebang(string code){
            int current = 0;
            int mStart;
            int miStart = -1;
            int pStart = -1;
            if(code[current] == '#' && code[++current] == '!'){
                mStart = current;
                while(code[current] != ';'){
                    if(code[current] == '.'){
                        if(miStart == -1){
                            miStart = current;
                        } else if(pStart == -1){
                            pStart = current;
                        } else{
                            throw new ExpaSyntaxError(-1,"Invalid Shebang at first line,");
                        }
                    }
                    current++;
                    
                }

            }
            return ("1", 1, 1, 1);
        }
        public static void Interpret(string fp, string? dbPath){
            string code = File.ReadAllText(fp);
            Lexer lexer = new(code);
            var tokenList = lexer.GetTokens();
            string exePath = (System.Reflection.Assembly.GetEntryAssembly()??throw new ExpaInterpreterError(-1, "Unable to implicitly find database path and database path not specified. Please specify database path as second argument.")).Location;
            dbPath ??= Path.GetDirectoryName(exePath)??throw new ExpaInterpreterError(-1, "Unable to implicitly find database path and database path not specified. Please specify database path as second argument.");
            dbPath += exePath.Split('.')[^1];
            Console.WriteLine(dbPath);
            FileHandler.FileHandler fileHandler = new(dbPath);
            Parser.SetParser(tokenList, fileHandler);
            var e = new ParseScope.ParseScope(Parser.unparsedScopes!["global"]);
            PPrinter.PPrint((ExpaNation)Parser.expaObjects["Icarus"]);
            PPrinter.PPrint((ExpaArea)Parser.expaObjects["IcarusHomeWorld"]);
        }
        public static void Main(string[] args){
            //TODO: SEMIURGENT: Load existing objects from disk\
            //TODO: ships have ship-classes, string, for easy print for ani
            string fp = """/workspaces/Expa/test.expa""";
            bool validDbFP = false;
            if(args.Length >= 2){
                foreach(var extension in Helpers.Defaults.VALIDDBFILEEXTENSIONS){
                    if(args[1].EndsWith(extension)){validDbFP = true;}
                }
            }
            Interpret(fp, validDbFP? args[1]: null);
            
        }
    }
}
