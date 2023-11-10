namespace New{
    using Commands;
    using Structs;
    using Errors;
    using Tokens;
    using ExpaObjects;
    using Parser;
    using ArgumentDict = Dictionary<Tokens.TokenType, Tokens.Token>;
    using BackgroundObjects;
    using Helpers;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;
    using System.Security.Principal;

    public class New: Commands{
        TokenType type;
        public New(CodeParseTransferrer input): base(input){
            current++;
            type = code[current].tokenType;
            Parse();
        }
        public static readonly HashSet<string> identifiers = new();
        public override void Parse(){
            //new
            //parse all stuff before going into type-specific stuff
            
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
            string display = args is not null? (args.TryGetValue(TokenType.DISPLAY, out Token? temp)? temp.lexeme! : identifier) : identifier;//identifier if not specified else use specified
            string? comment = null;
            if(args is not null){
                args.TryGetValue(TokenType.COMMENT, out Token? commentV);
                comment = commentV is not null? commentV!.literal! : null;
            }
            switch(type){
                case TokenType.TEMPLATE: Template(args, identifier, display, comment); break;
                case TokenType.NATION: Nation(args, identifier, display, comment); break; 
                case TokenType.AREA: Area(args, identifier, display, comment); break;
                case TokenType.FUNCTION: Function(args, identifier, display, comment); break;
            }
            parent.AddChild(identifier);
            Parser.expaObjects[identifier].AddParent(parent.TokenIdentifier.lexeme);
            new ParseScope.ParseScope(Parser.unparsedScopes![identifier]);
        }
                private void Template(ArgumentDict? args, string identifier, string display, string? comment){
            if(args == null){
                //backtrack. If at any point it is unclear, throw an error.
                if(parent.children.Contains(identifier)){throw new ExpaReassignmentError(code[current].line);}
                Parser.expaObjects[identifier] = new ExpaTemplate(parent, Parser.unparsedScopes![identifier], parent is ExpaNation? (ExpaNation)parent: GetNationParent(parent) ,false, display, comment);
                
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
            
        }

        private void Nation(ArgumentDict? args, string identifier, string display, string? comment){
            if(args == null){
                if(parent is not ICanBeParent<ExpaNation> newParent){
                throw new ExpaArgumentError(code[current].line, "Nation can only be a child of `global` or `nation` || Unable to determine exact error location, error occured around: ");
                }
                Parser.expaObjects[identifier] = new ExpaNation(
                    newParent,
                    Parser.unparsedScopes[identifier],
                    ((IHasTime)parent).Time,
                    display: display,
                    comment: comment
                );
                return;
            }
            ICanBeParent<ExpaNation> lParent;
            try{
                 lParent = (ICanBeParent<ExpaNation>)(args!.TryGetValue(TokenType.IPARENT, out Token? pValue)? Parser.expaObjects[pValue!.lexeme]:parent  );
            } catch(KeyNotFoundException){
                throw new ExpaSyntaxError(args[TokenType.IPARENT].line, "Parent referenced has not been instantiated or is an invalid token");
            } catch(InvalidCastException){
                throw new ExpaArgumentError(code[current].line, $"Error near definition of {identifier} - invalid parent, please explicitly define a valid immediate parent. possibly near");
            }

            Parser.expaObjects[identifier] = new ExpaNation(
                lParent,
                Parser.unparsedScopes[identifier],
                args.TryGetValue(TokenType.TIME, out Token? value)? //if there is specified time, use the specified time(goto immeidate purple brackets)
                /* #region */
                    (
                        value.tokenType == TokenType.NUMBER?//is it a number-like token? if yes, then parse it as a decimal AC time.
                        BackgroundObjects.Time.ParseAcTime(value.literal!):
                        /* #region */
                        (
                            value.tokenType == TokenType.MONTHTIME?//if it is month-like, then parse it as a month-like token
                            Time.ParseMonthTime(value.literal!)://if not,
                            /*#region */
                            (
                                /*#region*/
                                (
                                    value.tokenType == TokenType.IDENTIFIER//if it is an identifier - it could possibly be a time reference
                                    &&
                                    Parser.expaObjects.TryGetValue(value.lexeme, out ExpaObject? TValue)//and it actually is an identifier
                                    &&
                                    TValue is ExpaTime time//and the referenced object is a User-accessable time object
                                )?/*#endregion*/
                                time.Time://if so, return it as a Background-Time object
                                throw new ExpaArgumentError(value.line, "Invalid argument for `time`")//invalid because can't parse and isn't a identifier which is a time reference.
                            ) /*#endregion*/
                        )/*#endregion*/
                    ): /* #endregion */
                ((BackgroundObjects.IHasTime)parent).Time, 
                args.TryGetValue(TokenType.MINSIZE, out Token? minSizeV)? IsValidSize(minSizeV) : Defaults.MINCSS,
                args.TryGetValue(TokenType.MAXSIZE, out Token? maxSizeV)? IsValidSize(maxSizeV): Defaults.MAXCSS, //IsValidSize returns int
                args.TryGetValue(TokenType.DISPLAY, out Token? displayV)? displayV.literal! : null,
                args.TryGetValue(TokenType.COMMENT, out Token? commentV)? commentV.literal!: null
            );
            return;
        }
        
        private void Area(ArgumentDict? args, string identifier, string display, string? comment){
            if(args == null){
                try{
                    Parser.expaObjects[identifier] = new ExpaArea((ICanBeParent<ExpaArea>)parent, (ExpaNation)parent, Defaults.MINCSS, Defaults.MAXCSS, Parser.unparsedScopes[identifier], display, comment);
                    return;
                } catch(InvalidCastException){
                    throw new ExpaArgumentError(code[current].line, "Invalid parent or nation; please specify a valid parent as per the docs or specify a valid nation parent. Near");
                } catch(KeyNotFoundException){
                    throw new ExpaSyntaxError(code[current].line, $"Could not find a defnition corresponding to the area {identifier}. Near");
                }
            }
            
            ExpaNation nation;//find nation parent
            try{
                nation = args!.TryGetValue(TokenType.NATION , out Token? nationIToken)? (ExpaNation)Parser.expaObjects[nationIToken.lexeme] : (parent is ExpaNation? (ExpaNation)parent: GetNationParent(parent)); // if nation specified, use it; else if parent is a nation then use that else trace. Catch invalid identifier.
            } catch(KeyNotFoundException){
                throw new ExpaArgumentError(args![TokenType.NATION].line, "Invalid identifier");
            }
            ICanBeParent<ExpaArea> parentV; //find normal parent
            try{
                parentV = args.TryGetValue(TokenType.IPARENT, out Token? IParentV)? (ICanBeParent<ExpaArea>)Parser.expaObjects[IParentV.lexeme] : (ICanBeParent<ExpaArea>)parent;
            } catch(KeyNotFoundException){
                throw new ExpaArgumentError(args[TokenType.IPARENT].line, "Invalid identifier - please instantiate before usage");
            } catch(InvalidCastException){
                throw new ExpaArgumentError(code[current].line, "No parent was specified and unable to implicitly specify a parent(parent namespace was not valid). Please specify a direct parent.Near ");
            }
            args.TryGetValue(TokenType.MAXSIZE, out Token? MaxSizeV);//define tempsize
            args.TryGetValue(TokenType.MINSIZE, out Token? MinSizeV);//define tempsize
            try{
                Parser.expaObjects[identifier] = new ExpaArea(parentV, nation,
                    MinSizeV is null? Defaults.MINCSS: IsValidSize(MinSizeV),//default if not specified, else parse
                    MaxSizeV is null? Defaults.MAXCSS: IsValidSize(MaxSizeV),
                    Parser.unparsedScopes[identifier],
                    display,
                    comment
                );
            } catch(KeyNotFoundException){
                throw new ExpaSyntaxError(code[current].line, $"There was no definition for {identifier} found. Error occurred near");
            }
        }
        
        private void Function(ArgumentDict? args, string identifier, string display, string? comment){
            try{
                Parser.expaObjects[identifier] = new ExpaFunction((ICanBeParent<ExpaFunction>)parent, Parser.unparsedScopes[identifier], display, comment);
                return;
            } catch(InvalidCastException){
                throw new ExpaArgumentError(code[current].line, "Invalid parent or nation; please specify a valid parent as per the docs or specify a valid nation parent. Near:");
            } catch(KeyNotFoundException){
                throw new ExpaArgumentError(code[current].line, $"Could not find a definition corresponding to the function {identifier}");
            }            
        }
        private static int IsValidSize(Token size){
            //if size is a number then parse and return else throw
            if(size.tokenType == TokenType.NUMBER){
                return int.Parse(size.literal!);
            } else{
                throw new ExpaArgumentError(size.line, $"Invalid argument for maxsize {size}(type {size.tokenType}, expected number)");
                }
        }
        private ArgumentDict ExtractArguments(ArgumentDict args){
            List<Token[]> argList = new();
            current++;
            start = current;
            TokenType i;
            while(code[current].tokenType!= TokenType.RIGHTPAREN){
                if(code[current].tokenType == TokenType.SEMICOLON){
                    argList.Add(code[start..current]);//no +1 on the current to skip the semicolon
                    start = current;
                }
                current++;
            }
            foreach(Token[] argLine in argList){
                if(argLine.Any(x => x.tokenType == TokenType.COLON)){
                    args[Keywords.IsValidArgumentName(i = argLine[0].tokenType)? i: throw new ExpaArgumentError(code[current].line,$"Invalid argument type {code[current].lexeme}")] = argLine[2];
                    //if there is a kwarg, but the kw is not valid, throw.
                } else{
                    args[(i = Converters.IntToTType(++current)) == TokenType.INTERPRETERNULL? throw new ExpaArgumentError(code[current].line, $"Max positional argument count exceeded(max: {current-1})"): i] = argLine[0];
                    //if the conversion returns InterpreterNull, that means that the conversion failed, and thus also means that the positional argument limit was exceeded.
                }
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