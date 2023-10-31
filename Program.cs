using System;
using System.Collections.Generic;
namespace interpreter{

    using lexer;
    using Helpers;
	using Errors;
    using System.IO;
    using BackgroundObjects;
    using Parser;

    public class Interpreter{
        public static void Interpret(string fp){
            string code = File.ReadAllText(fp);
            Lexer lexer = new(code);
            var tokenList = lexer.GetTokens();
            Console.WriteLine(tokenList.Length);
            PPrinter.PPrint(tokenList);	
            Parser.SetParser(tokenList);
            var e =new ParseScope.ParseScope(Parser.unparsedScopes!["global"]);
        }
        public static void Main(string[] args){
            //TODO: SEMIURGENT: Load existing objects from disk\
            //TODO: ships have ship-classes, string, for easy print for ani
            string fp = """D:\coding\c#\expa\Expa\main.expa""";
            
            Interpret("""D:\coding\c#\expa\Expa\test.expa""");
            
        }
    }
}
