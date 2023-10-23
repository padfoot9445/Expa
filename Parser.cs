using System;
namespace parser{
    
    using System.Data.Common;
    using helpers;
    using Tokens;
    using ExpaObjects;
    using Scope;
    public class Parser{
        private Token[] tokens;
        private readonly Dictionary<Token, Scope> unparsedScopes = new Dictionary<Token, Scope>();
        private readonly List<ExpaObject> expaObjects = new List<ExpaObject>();
        public Parser(Token[] aTokens){
            tokens = aTokens.Concat(new Token[]{new Token(TokenType.EOF, -1, "EOF inserted by interpreter", null)}).ToArray();
            unparsedScopes = ExtractNamespaceScope(tokens);
        }
        public void parseTokens(){
            
                
        }
        public Dictionary<Token, Scope> ExtractNamespaceScope(Token[] aTokens){
            int current = 0;
            int start = 0;
            Dictionary<Token, Scope> returnDict = new Dictionary<Token, Scope>();
            while(aTokens[current].tokenType != TokenType.EOF){
                current++;
                if(aTokens[current].tokenType == TokenType.LEFTBRACE){
                    start = current;			
                    int braceCount = 1;
                    while(braceCount != 0){
                        current++;
                        if(aTokens[current].tokenType == TokenType.RIGHTBRACE){
                            braceCount--;
                        } else if(aTokens[current].tokenType == TokenType.LEFTBRACE){
                            braceCount++;
                        }
                    }
                    if(aTokens[start - 1].tokenType == TokenType.GLOBAL){
                        returnDict[aTokens[start-1]] = new Scope(aTokens[start-1], aTokens[start-1].tokenType, aTokens.SubArray(start + 1, current));
                    } else{
                        switch(aTokens[start - 2].tokenType){//to account for the identifier
                            case TokenType.FUNCTION:
                            case TokenType.AREA:
                            case TokenType.TEMPLATE:
                            returnDict[aTokens[start - 1]] = new Scope(aTokens[start - 1], aTokens[start - 2].tokenType, aTokens.SubArray(start + 1, current)); //start + 1 is because we want to skip the curly braces; current and not current + 1 for the same reason
                                break;
                        }
                    }
                }
            }
            return returnDict;
        }
    }
    internal class parseScope{
        private readonly Scope scope;	
        private ExpaObject? parent;
        private bool __global;
        private Parser parser;
        private int current = 0;
        private readonly Token[] code;
        public parseScope(Scope aScope, Parser aParser,  ExpaObject aParent){
            scope = aScope;
            parent = aParent;
            __global = false;
            parser = aParser;
            code = scope.Code;
            //parser for the unparsed scopes; 
        }
        public parseScope(Scope aScope, Parser aParser, TokenType input){
            if(input != TokenType.GLOBAL){
                throw new mainException("Error whilst parsing. Function argument error; contact padfoot9445 on discord.");
            }
            scope = aScope;
            __global = true;
            parser = aParser;
            code = scope.Code;
        }
        private readonly struct type_and_identifier{
            public readonly TokenType type;
            public readonly Token identifier;
            public type_and_identifier(TokenType aType, Token aIdentifier){
                type = aType;
                identifier = aIdentifier;
            }
        }
        private void parse(){
            int start = 0;
            int length = code.Length;
            while(current < length){
                switch(code[current].tokenType){
                    case TokenType.NEW:
                        current++;
                        switch(code[current].tokenType){
                            case TokenType.TEMPLATE:
                            case TokenType.FUNCTION:
                            case TokenType.AREA:
                            case TokenType.NATION:
                                //functions that will have a seperate scope to themselves
                               
                            case TokenType.SHIPYARD:
                                break;
                        }
                        break;
                }
            }
        }
        private bool hasArgument(){
             TokenType __type = code[current].tokenType;
            current++;
            string identifier = code[current].lexeme;
            current++;
            if(identifier == null){
                throw new ExpaSyntaxError(code[current].line,$"Expected identifier, got {code[current].tokenType}");
            }
            if(code[current].tokenType == TokenType.LEFTPAREN){
                return true;
            } else{
                return false;
            }
    }
    
        
    }
}