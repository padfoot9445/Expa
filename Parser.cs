using System;
namespace parser{
    
    using System.Data.Common;
    using helpers;
    using Tokens;
    using ExpaObjects;
    using Scope;
    public class Parser{
        private Token[] tokens;
        private readonly Dictionary<string, Scope> unparsedScopes = new();
        private readonly List<ExpaObject> expaObjects = new();
        public ExpaGlobal expaGlobal;
        public Parser(Token[] aTokens){
            tokens = aTokens.Concat(new Token[]{new(TokenType.EOF, -1, "EOF inserted by interpreter", null)}).ToArray();
            unparsedScopes = ExtractNamespaceScope(tokens);
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
                        expaGlobal = new ExpaGlobal(returnDict[aTokens[start-1].lexeme]);
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
    internal class ParseScope{
        private readonly Scope scope;	
        private ExpaNameSpace parent;
        private bool __global;
        private Parser parser;
        private int current = 0;
        private readonly Token[] code;
        private readonly ExpaNameSpace self;
        public ParseScope(Scope aScope, Parser aParser,  ExpaNameSpace aParent){
            scope = aScope;
            parent = aParent;
            __global = aParent.TokenIdentifier.tokenType == TokenType.GLOBAL? false: true;
            parser = aParser;
            code = scope.Code;
            self = (ExpaNameSpace)parent.children[scope.TokenIdentifier.lexeme];
            //parser for the unparsed scopes; 
        }
        
        private void Parse(){
            int start = 0;
            int length = code.Length;
            while(current < length){
                //for every token
                switch(code[current].tokenType){
                    //switch
                    case TokenType.NEW:
                        //new
                        //parse all stuff before going into type-specific stuff
                        current++;
                        TokenType type = code[current].tokenType;
                        current++;
                        string identifier = code[current].lexeme ?? throw new ExpaSyntaxError(code[current].line,$"Expected identifier, got {code[current].tokenType}");
                        Dictionary<TokenType, Token>? args = HasArgument()? new(): null;
                        if(code[current + 1].tokenType == TokenType.RIGHTPAREN){
                            args = null;
                            current++;
                        } else if(args != null){
                            while(code[current].tokenType != TokenType.RIGHTPAREN){
                                if(!Keywords.argumentNames.Contains(code[current].tokenType)){
                                    throw new ExpaSyntaxError(code[current].line, $"Invalid argument type {code[current].lexeme}");
                                } else if(code[current + 1].tokenType != TokenType.COLON){
                                    throw new ExpaSyntaxError(code[current + 1].line, $"Invalid token(Expected ':', got {code[current+1].lexeme} instead)");
                                } else if(code[current + 3].tokenType != TokenType.SEMICOLON && code[current + 3].tokenType != TokenType.RIGHTPAREN){
                                    throw new ExpaSyntaxError(code[current + 3].line, $"Expected semicolon, got {code[current + 3].lexeme}");
                                }
                                args[code[current].tokenType] = code[current + 2];
                                if(code[current + 3].tokenType == TokenType.RIGHTPAREN){current += 4; break;}
                                current += 4;
                            }
                        }
                        switch(type){
                            case TokenType.TEMPLATE:
                                if(args == null){
                                    //backtrack. If at any point it is unclear, throw an error.
                                    parent.AddChild(new ExpaTemplate(parent, scope, parent is ExpaNation? (ExpaNation)parent: getNationParent(parent) ,false));
                                } else{
                                    parent.AddChild(
                                        new ExpaTemplate(parent, scope, 
                                            args.ContainsKey(TokenType.NATION)? //if nation is specified
                                                (ExpaNation)parent.children[args[TokenType.NATION].lexeme]://pass the specified nation as an object by using the parent.children dictionary and the token from args
                                               
                                                    parent is ExpaNation ? (ExpaNation)parent: //elif immediate parent is an expanation
                                                     getNationParent(parent), // else try to get the parent
                                            args.TryGetValue(TokenType.EQUALIZE, out var value) == true?//if equalize is specified
                                            (value.tokenType == TokenType.TRUE?
                                                 true: 
                                                 false): 
                                            false ));
                                }
                                break;
                            case TokenType.FUNCTION:

                            case TokenType.AREA:
                            case TokenType.NATION:                                
                            case TokenType.SHIPYARD:
                                break;
                        }
                        break;
                }
            }
        }
        private ExpaNation getNationParent(ExpaObject input){
            //input target object
            ExpaNation? nation = null;
            ExpaNation? tempNation = null;
            //where there is a nation in the lineage, that is the nation. else, we keep going
            if(parent.parents.ContainsKey("global")){throw new ExpaSyntaxError(code[current].line, $"Unable to backtrace valid nation origin, please explicitly declare the nation");}
            foreach(var value in input.parents.Values){
                if(value is ExpaNation){
                    if(nation == null){
                        nation = (ExpaNation)value;
                    } else{
                        nation = null;
                        throw new ExpaSyntaxError(code[current].line, $"Unable to backtrace valid nation origin, please explicitly declare the nation");
                    }
                } else{
                    if(tempNation == null){
                        //if there is no direct nation parent yet, set tempnation to parent(if there is no valid parent down that path then error is thrown there)
                        tempNation = getNationParent(value);
                    } else if(tempNation != getNationParent(value)){//if we have conflicting nation parents
                        throw new ExpaSyntaxError(code[current].line, $"Unable to backtrace valid nation origin, please explicitly declare the nation");
                    } 
                }
            }
            //return the non-null value; if both are null then throw
            return nation ?? tempNation ??  throw new ExpaSyntaxError(code[current].line, $"Unable to backtrace valid nation origin, please explicitly declare the nation");
        }
        private bool HasArgument(){
        current++;
        if(code[current].tokenType == TokenType.LEFTPAREN){
            return true;
        } else if(code[current].tokenType == TokenType.SEMICOLON){
            return false;
        } else{
            throw new ExpaSyntaxError(code[current].line, $"Expected parenthesis or semicolon, got {code[current].tokenType}");
        }
        }
        
    }
    
        
    }
