using BackgroundObjects;
using Tokens;

namespace Structs
{
    internal readonly struct Scope
    {
        public readonly string Identifier { get; init; }
        public readonly TokenType TType { get; init; }
        public readonly Token[] Code { get; init; }
        public ArgumentList? Arguments{ get; }
        public Scope(string Identifier, TokenType aType, Token[] aCode)
        {
            this.Identifier = Identifier;
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