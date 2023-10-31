using System;
namespace Parser{
    
    using System.Data.Common;
    using Helpers;
	using Errors;
    using Tokens;
    using ExpaObjects;
    using Structs;
    public class Parser{
        public static Token[] tokens{get; private set;} = Array.Empty<Token>();
        public static Dictionary<string, Scope> unparsedScopes{get; private set;} = new();
        public static readonly Dictionary<string, ExpaObject> expaObjects = new();
        public ExpaGlobal? expaGlobal;
        public static void SetParser(Token[] aTokens){//due to fileHandler reasons, we extract the scopes before initializing the Parser object
            tokens = aTokens.Concat(new Token[]{new(TokenType.EOF, -1, "EOF inserted by interpreter", null)}).ToArray();
            Parser.unparsedScopes = ExtractNamespaceScope(tokens);
            //load all expa objects from storage
            foreach(var scope in unparsedScopes.Values){
                expaObjects[scope.TokenIdentifier.lexeme] = new ExpaGlobal(scope, new(0,0));
            }
            
        }
        public static void ParseTokens(){
        }
        public static Dictionary<string, Scope> ExtractNamespaceScope(Token[] aTokens){
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
                                break;
                            case TokenType.AREA:
                                break;
                            case TokenType.TEMPLATE:
                            //TODO: Finish
                            returnDict[aTokens[start - 1].lexeme] = new Scope(aTokens[start - 1], aTokens[start - 2].tokenType, aTokens.SubArray(start + 1, current)); //start + 1 is because we want to skip the curly braces; current and not current + 1 for the same reason
                                break;
                            default:
                                throw new ExpaSyntaxError(aTokens[start-2].line, $"Expected Type, got {aTokens[start - 2]} of type {aTokens[start - 2].tokenType}");
                        }
                    }
                }
            }
            return returnDict;
        }
    }
    
    
        
    }
