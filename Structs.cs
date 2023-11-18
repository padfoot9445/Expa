

namespace Structs
{
    using Tokens;
    using Markers;
    using Interfaces;
    using BackgroundObjects;
    public readonly struct TypeAndIdentifier{
            public readonly TokenType type;
            public readonly Token identifier;
            public TypeAndIdentifier(TokenType aType, Token aIdentifier){
                type = aType;
                identifier = aIdentifier;
            }
        }

    public readonly struct CodeParseTransferrer{
        public readonly int current;
        public readonly Token[] tokenList;
        public readonly BaseExpaNameSpace parent;
        public CodeParseTransferrer(int current, Token[] tokenList, BaseExpaNameSpace parent){
            this.current = current;
            this.tokenList = tokenList;
            this.parent = parent;
        }
    }
    public readonly struct Result: Interfaces.Result{
        public BaseExpaObject expaObject{ get; init; }
        public readonly string ParentStringID;
        public readonly string[] ChildStringIDs;
        public Result(BaseExpaObject expaObject, string ParentStringID, string[] ChildStringIDs, string type){
            this.expaObject = expaObject;
            this.ParentStringID = ParentStringID;
            this.ChildStringIDs = ChildStringIDs;
        }
    }
    public readonly struct InitializedResult: Interfaces.Result{
        public BaseExpaObject expaObject{ get; init; }
        public InitializedResult(BaseExpaObject expaObject) => this.expaObject = expaObject;
    }
    public readonly struct BackgroundArgument{
       public readonly string name;
       public readonly TokenType type;
       public readonly int position;

    }

}