namespace Scope{
    using Tokens;
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
}