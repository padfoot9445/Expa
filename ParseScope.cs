namespace ParseScope
{
    using Structs;
    using Parser;
    using Tokens;
    using Interfaces;
    using New;
    using ExpaObjects;

    internal class ParseScope{
        private Scope Scope{ get; init; }
        private int Current { get; set; } = 0;
        private Token[] Code{ get; init; }
        private IExpaNameSpace Self{ get; init; }
        private bool IsGlobal { get; init; } = false;
        private string ID{ get; init; }//id of the scope we are parsing now
        private TokenType Type{ get; init; }
        internal ParseScope(Scope Scope, string currentScopeID){
            this.Scope = Scope;
            this.Code = Scope.Code;
            this.ID = currentScopeID;
            this.Self = (IExpaNameSpace)Parser.expaObjects[ID];
            this.Type = Scope.TType;
            if(Self is ExpaGlobal){
                IsGlobal = true;
            }
            //Parser for the unparsed scopes; 
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