namespace ParseScope{
    using Structs;
    using ExpaObjects;
    using Parser;
    using Tokens;
    using Helpers;
	using Errors;
    using New;
    public class ParseScope{
        private readonly Scope scope;
        private int current = 0;
        private readonly Token[] code;
        private readonly ExpaNameSpace self;
        public ParseScope(Scope aScope){
            scope = aScope;
            code = scope.Code;
            self = (ExpaNameSpace)Parser.expaObjects[scope.TokenIdentifier.lexeme];
            Parser.unparsedScopes!.Remove(scope.TokenIdentifier.lexeme);
            if(scope.TType == TokenType.TEMPLATE){
                ParseTemplate();
            } else{
                Parse();    
            }
            //Parser for the unparsed scopes; 
        }
        
        private void Parse(){
            int start = 0;
            int length = code.Length;
            while(current < length){
                //for every token; 
                switch(code[current].tokenType){
                    //switch
                    case TokenType.NEW:current += ParseNewExpr(); break;
                }
                current++;
            }
        }
        private void ParseTemplate(){
            Console.WriteLine("Parsing template...");
            return;
        }
        private int ParseNewExpr(){
            return new New(new CodeParseTransferrer(current, code, self)).Increment();
        }
        
        
        
    }
    
}