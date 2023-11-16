

namespace Structs
{
    using Tokens;
    using Metadata;
    using Interfaces;
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
        public readonly INameSpace parent;
        public CodeParseTransferrer(int current, Token[] tokenList, INameSpace parent){
            this.current = current;
            this.tokenList = tokenList;
            this.parent = parent;
        }
    }
    public readonly struct Result{
        public readonly IExpaObject expaObject;
        public readonly string ParentStringID;
        public readonly string[] ChildStringIDs;
        public Result(IExpaObject expaObject, string ParentStringID, string[] ChildStringIDs){
            this.expaObject = expaObject;
            this.ParentStringID = ParentStringID;
            this.ChildStringIDs = ChildStringIDs;
        }
    }
    public readonly struct BackgroundArgument{
       public readonly string name;
       public readonly TokenType type;
       public readonly int position;

    }

}