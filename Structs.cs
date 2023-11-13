

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
        public readonly string[] parentIdentifiers;
        public readonly string[]? childIdentifiers;
        public Result(IExpaObject expaObject, string[] parentIdentifiers, string[] childIdentifiers){
            this.expaObject = expaObject;
            this.parentIdentifiers = parentIdentifiers;
            this.childIdentifiers = childIdentifiers;
        }
    }
    public readonly struct BackgroundArgument{
       public readonly string name;
       public readonly TokenType type;
       public readonly int position;

    }

}