namespace New{
    using Commands;
    using Structs;
    using Errors;
    using Tokens;
    using ExpaObjects;
    using parser;

    public class New: Commands{
        public New(CodeParseTransferrer input, parser.Parser parser): base(input, parser){Parse();}
        public override void Parse(){
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
                //if HasArgument has detected arguments, start parsing arguments
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
                case TokenType.TEMPLATE: Template(args, identifier); break;
            }
        }
        private void Template(Dictionary<TokenType, Token>? args, string identifier){
            if(args == null){
                //backtrack. If at any point it is unclear, throw an error.
                //TODO: No fucking clue why I directly passed the scope, change to the extracted scope, parse it as well
                parent.AddChild(new ExpaTemplate(parent, parser.unparsedScopes[identifier], parent is ExpaNation? (ExpaNation)parent: getNationParent(parent) ,false));
            } else{
                parent.AddChild(
                    new ExpaTemplate(parent, parser.unparsedScopes[identifier], 
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
            new ParseScope.ParseScope(parser.unparsedScopes[identifier],parser, self);
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
    }
}