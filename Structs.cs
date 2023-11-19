

namespace Structs
{
    using Tokens;
    using Markers;
    using Interfaces;
    using BackgroundObjects;
    internal readonly struct TypeAndIdentifier{
            public readonly TokenType type;
            public readonly Token identifier;
            public TypeAndIdentifier(TokenType aType, Token aIdentifier){
                type = aType;
                identifier = aIdentifier;
            }
        }

    internal readonly struct CodeParseTransferrer{
        public readonly int current;
        public readonly Token[] tokenList;
        public readonly BaseExpaNameSpace parent;
        public CodeParseTransferrer(int current, Token[] tokenList, BaseExpaNameSpace parent){
            this.current = current;
            this.tokenList = tokenList;
            this.parent = parent;
        }
    }
    internal readonly struct Result{
        public BaseExpaObject ExpaObject{ get; init; }
        public readonly string? ParentStringID;
        public readonly string[] ChildStringIDs;
        public Result(BaseExpaObject expaObject, string? ParentStringID, string[] ChildStringIDs){
            this.ExpaObject = expaObject;
            this.ParentStringID = ParentStringID;
            this.ChildStringIDs = ChildStringIDs;
        }
    }
    internal readonly struct BackgroundArgument{
       public readonly string name;
       public readonly TokenType type;
       public readonly int position;

    }

}