namespace Structs{
    using Tokens;
    using ExpaObjects;
    public readonly struct Scope{
            public readonly Token __identifier;
            public readonly TokenType __type;
            public readonly Token[] __code;
            public Scope(Token aIdentifier, TokenType aType, Token[] aCode){
                __identifier = aIdentifier;
                __type = aType;
                __code = aCode;
            }
            public Token TokenIdentifier{
                get{
                    return __identifier;
                }
            }
            public TokenType TType{
                get{
                    return __type;
                }
            }
            public Token[] Code{
                get{return __code;}
            }
        }
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
        public readonly ExpaNameSpace parent;
        public readonly ExpaNameSpace self;
        public CodeParseTransferrer(int current, Token[] tokenList, ExpaNameSpace parent, ExpaNameSpace self){
            this.current = current;
            this.tokenList = tokenList;
            this.parent = parent;
            this.self = self;
        }
    }
}