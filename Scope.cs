using BackgroundObjects;
using Tokens;

namespace Structs
{
    internal readonly struct Scope
    {
        public readonly Token TokenIdentifier { get; init; }
        public readonly TokenType TType { get; init; }
        public readonly Token[] Code { get; init; }
        public ArgumentList? Arguments{ get; }
        public Scope(Token aIdentifier, TokenType aType, Token[] aCode)
        {
            TokenIdentifier = aIdentifier;
            Code = aCode;
            TType = aType;
        }
    }
    internal struct Argument
    {
        public TokenType ArgType { get; init; }
        public string ArgIdentifier { get; init; }
        public int Position { get; set; }
    }
}