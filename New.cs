namespace New{
    using Commands;
    using Structs;
    using Errors;
    using Tokens;
    using ExpaObjects;
    using Parser;
    using ArgumentDict = Dictionary<Tokens.TokenType, Tokens.Token>;

    public class New: Commands{
        public New(CodeParseTransferrer input): base(input){Parse();}
        public static readonly HashSet<string> identifiers = new();
        public override void Parse(){
            Console.WriteLine("pnew");
            //new
            //parse all stuff before going into type-specific stuff
            current++;
            TokenType type = code[current].tokenType;
            current++;
            if(code[current].tokenType != TokenType.IDENTIFIER){throw new ExpaSyntaxError(code[current].line,$"Expected identifier, got {code[current].tokenType}");}
            string identifier = code[current].lexeme;
            if(identifiers.Contains(identifier)){
                throw new ExpaNameError(code[current].line, $"Identifier cannot be used anywhere else(even outside of scope) {code[current].lexeme}");
            }
            ArgumentDict? args = HasArgument()? new(): null;
            if(code[current + 1].tokenType == TokenType.RIGHTPAREN){
                args = null;
                current++;
            } else if(args != null){
                //if HasArgument has detected arguments, start parsing arguments
                args = ExtractArguments(args);
            }
            switch(type){
                case TokenType.TEMPLATE: Template(args, identifier); break;
            }
        }
        private void Template(ArgumentDict? args, string identifier){
            if(args == null){
                //backtrack. If at any point it is unclear, throw an error.
                //TODO: No fucking clue why I directly passed the scope, change to the extracted scope, parse it as well
                if(parent.children.Contains(identifier)){throw new ExpaReassignmentError(code[current].line);}
                Parser.expaObjects[identifier] = new ExpaTemplate(parent, Parser.unparsedScopes![identifier], parent is ExpaNation? (ExpaNation)parent: GetNationParent(parent) ,false);
                
            } else{
                try{
                    Parser.expaObjects[identifier] = new ExpaTemplate(parent, Parser.unparsedScopes![identifier], 
                            args.ContainsKey(TokenType.NATION)? //if nation is specified
                                (ExpaNation)Parser.expaObjects[args[TokenType.NATION].lexeme]://pass the specified nation as an object by using the parent.children dictionary and the token from args
                            
                                    parent is ExpaNation ? (ExpaNation)parent: //elif immediate parent is an expanation
                                    GetNationParent(parent), // else try to get the parent
                         args.TryGetValue(TokenType.EQUALIZE, out var value) == true?//if equalize is specified
                            (value.tokenType == TokenType.TRUE?
                                   true: 
                                  false): 
                            false);
                } catch(/*KeyNotFoundException*/DivideByZeroException){
                    Console.WriteLine("what the fuck");
                }
            }
            parent.AddChild(identifier);
            new ParseScope.ParseScope(Parser.unparsedScopes![identifier]);
        }

        private void Nation(ArgumentDict? args, string identifier){
            if(args == null){
                Parser.expaObjects[identifier] = new ExpaNation(
                    (ExpaNameSpace)Parser.expaObjects["global"],
                    Parser.unparsedScopes[identifier],
                    ((ExpaGlobal)Parser.expaObjects["global"]).Time
                );
                return;
            }
            ExpaNameSpace parent;
            try{
                 parent = (ExpaNameSpace)(args!.TryGetValue(TokenType.IPARENT, out Token? value)? Parser.expaObjects["global"] : Parser.expaObjects[value!.lexeme]);
            } catch(KeyNotFoundException){
                throw new ExpaSyntaxError(args[TokenType.IPARENT].line, "Parent referenced has not been instantiated");
            }

            Parser.expaObjects[identifier] = new ExpaNation(
                parent,
                Parser.unparsedScopes[identifier],
                ((BackgroundObjects.IHasTime)parent).Time,
                args.TryGetValue(TokenType.DISPLAY, out Token? displayV)? displayV.literal! : null,
                args.TryGetValue(TokenType.COMMENT, out Token? commentV)? commentV.literal!: null
            );
            return;
        }
        private ArgumentDict ExtractArguments(ArgumentDict args){
            current++;
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
            return args;
        }
        private ExpaNation GetNationParent(ExpaObject input){
            //input target object
            ExpaNation? nation = null;
            ExpaNation? tempNation = null;
            //where there is a nation in the lineage, that is the nation. else, we keep going
            if(parent.parents.Contains("global")){throw new ExpaSyntaxError(code[current].line, $"Unable to backtrace valid nation origin, please explicitly declare the nation");}
            ExpaObject value;
            foreach(string sv in input.parents){
                value = Parser.expaObjects[sv];
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
                        tempNation = GetNationParent(value);
                    } else if(tempNation != GetNationParent(value)){//if we have conflicting nation parents
                        throw new ExpaSyntaxError(code[current].line, $"Unable to backtrace valid nation origin, please explicitly declare the nation");
                    } 
                }
            }
            //return the non-null value; if both are null then throw
            return nation ?? tempNation ??  throw new ExpaSyntaxError(code[current].line, $"Unable to backtrace valid nation origin, please explicitly declare the nation");
        }
    }
}