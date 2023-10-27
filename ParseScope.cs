namespace ParseScope{
    using Structs;
    using ExpaObjects;
    using parser;
    using Tokens;
    using Helpers;
	using Errors;
    public class ParseScope{
        private readonly Scope scope;	
        private ExpaNameSpace parent;
        private bool __global;
        private Parser parser;
        private int current = 0;
        private readonly Token[] code;
        private readonly ExpaNameSpace self;
        public ParseScope(Scope aScope, Parser aParser,  ExpaNameSpace aParent){
            scope = aScope;
            parent = aParent;
            __global = aParent.TokenIdentifier.tokenType == TokenType.GLOBAL? false: true;
            parser = aParser;
            code = scope.Code;
            self = (ExpaNameSpace)parent.children[scope.TokenIdentifier.lexeme];
            parser.unparsedScopes.Remove(scope.TokenIdentifier.lexeme);
            Parse();
            //parser for the unparsed scopes; 
        }
        
        private void Parse(){
            int start = 0;
            int length = code.Length;
            while(current < length){
                //for every token
                switch(code[current].tokenType){
                    //switch
                    case TokenType.NEW: New(); break;
                }
            }
        }
        private void New(){
            
            
        }
        
        
        
    }
}