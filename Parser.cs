using System;
namespace parser{
    
    using System.Data.Common;
    using Helpers;
	using Errors;
    using Tokens;
    using ExpaObjects;
    using Structs;
    public class Parser{
        private Token[] tokens;
        public readonly Dictionary<string, Scope> unparsedScopes = new();
        private readonly List<ExpaObject> expaObjects = new();
        public ExpaGlobal expaGlobal;
        public Parser(Token[] aTokens){
            tokens = aTokens.Concat(new Token[]{new(TokenType.EOF, -1, "EOF inserted by interpreter", null)}).ToArray();
            unparsedScopes = ExtractNamespaceScope(tokens);
            //load all expa objects from storage
        }
        public void ParseTokens(){
        }
        public Dictionary<string, Scope> ExtractNamespaceScope(Token[] aTokens){
            int current = 0;
            int start = 0;
            Dictionary<string, Scope> returnDict = new();
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
                        returnDict[aTokens[start-1].lexeme] = new Scope(aTokens[start-1], aTokens[start-1].tokenType, aTokens.SubArray(start + 1, current));
                    } else{
                        switch(aTokens[start - 2].tokenType){//to account for the identifier
                            case TokenType.FUNCTION:
                            case TokenType.AREA:
                            case TokenType.TEMPLATE:
                            //TODO: Finish
                            returnDict[aTokens[start - 1].lexeme] = new Scope(aTokens[start - 1], aTokens[start - 2].tokenType, aTokens.SubArray(start + 1, current)); //start + 1 is because we want to skip the curly braces; current and not current + 1 for the same reason
                                break;
                        }
                    }
                }
            }
            return returnDict;
        }
    }
    
    
        
    }
