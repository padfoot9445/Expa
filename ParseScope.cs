namespace ParseScope
{
    using System;
    using Structs;
    using Parser;
    using Tokens;
    using Interfaces;
    using ExpaObjects;
    using Errors;
    using Commands;

    internal class ParseScope{
        private Scope Scope{ get; init; }
        private int Current { get; set; } = 0;
        private int Start { get; set; } = 0;
        private Token[] Code{ get; init; }
        private IExpaNameSpace Self{ get; init; }
        private bool IsGlobal { get; init; } = false;
        private string ID{ get; init; }//id of the scope we are parsing now
        private TokenType Type{ get; init; }
        private int Length{get; init;}
        internal ParseScope(Scope Scope, string currentScopeID){
            this.Scope = Scope;
            this.Code = Scope.Code;
            this.ID = currentScopeID;
            this.Self = (IExpaNameSpace)Parser.expaObjects[ID];
            this.Type = Scope.TType;
            this.Length = Code.Length;
            if(Self is ExpaGlobal){
                IsGlobal = true;
            }
            switch(this.Type){
                case TokenType.GLOBAL:
                case TokenType.NATION:
                case TokenType.AREA:
                    Parse();
                    break;
            }
            //Parser for the unparsed scopes; 
        }
        
        private void Parse(){
            while(Current < Length){
                if(Code[Current].IsValueType()){
                    ParseNewVar();
                } else if(Code[Current].IsCommand()){
                    switch(Code[Current].Type){
                        case TokenType.NEW: ParseNewVar(); continue;
                        case TokenType.VIEW: ParseView(); continue;
                        case TokenType.USING: ParseUsing(); continue;
                        case TokenType.FOR: ParseFor(); continue;
                        case TokenType.FOREACH: ParseForEach(); continue;
                        case TokenType.PERMANENT: ParsePermanent(); continue;
                        case TokenType.WHILE: ParseWhile(); continue;
                        default: throw new ExpaInterpreterError(-1, $"reached default case in {nameof(Parse)}, IsCommand.");
                    }
                }
            }

        }

        private bool IsAtEnd() => Current > Length;
        private bool CodeIsLongEnough(int required) => Length - Current < required ? throw new ExpaSyntaxError(Code[Current].Line, "End of file while parsing expression, statement, or command") : true;
        private Token[] ExtractCurrentLine(){
            //extracts the rest of the current line, or if Code[Current] points to the end of a line, the next line
            Start = Current;
            while(Code[Current++].Type != TokenType.SEMICOLON){
                    if(IsAtEnd()){
                        throw new ExpaSyntaxError(Code[Start].Line, "Missing semicolon. after or ");
                    }
            }
            return Code[Start..Current];
        }
        private Token[] ExtractBrackets(TokenType opening, TokenType closing){
            //starting from the current token, which must be a round bracket, extract everything within the round bracket
            if(Code[Current].Type != opening){
                throw new ExpaSyntaxError(Code[Current].Line, $"Invalid Token {Code[Current].Lexeme}");
            }
            Start = ++Current;
            int stack = 1;
            while(stack > 0){
                if(Code[Current].Type == closing){ stack--; }
                else if(Code[Current].Type == opening){ stack++; }
                Current++;
                if(IsAtEnd()){
                    throw new ExpaSyntaxError(Code[Current].Line, "Missing closing bracket ')' after or");
                }
            }
            return Code[Start..Current];

        }
        private Token[] ExtractRoundBrackets() => ExtractBrackets(TokenType.LEFTPAREN, TokenType.RIGHTPAREN);
        private Token[] ExtractBraces() => ExtractBrackets(TokenType.LEFTBRACE, TokenType.RIGHTBRACE);
        //any form of new variable statment will be handled by the New class instead of here
        private void ParseNewVar() => new New(ExtractCurrentLine(), Self.StringID).Execute();
        private void ParseView() => new View(ExtractCurrentLine(), Self.StringID).Execute();
        private void ParseUsing() => new Using(ExtractCurrentLine(), Self.StringID).Execute();
        private void ParseFor(){
            Current++;
            new For(ExtractRoundBrackets(), ExtractBraces(), Self.StringID).Execute();
        }
        private void ParseForEach(){
            Current++;
            new ForEach(ExtractRoundBrackets(), ExtractBraces(), Self.StringID).Execute();
        }
        private void ParsePermanent(){
            Current++;
            new Permanent(ExtractBraces(), Self.StringID).Execute();
        }
        private void ParseWhile(){
            Current++;
            new While(ExtractRoundBrackets(), ExtractBraces(), Self.StringID).Execute();
        }
        
    }
    
}