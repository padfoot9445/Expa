
using Structs;
using Errors;
using Tokens;
using ExpaObjects;
using ArgumentDict = System.Collections.Generic.Dictionary<Tokens.TokenType, Tokens.Token>;
using BackgroundObjects;
using Helpers;
using Markers;
using Interfaces;

namespace New
{
    using Commands;
    public class New: Commands{
        TokenType type;
        public New(CodeParseTransferrer input): base(input){
            current++;
            type = code[current].Type;
            Parse();
        }
        public static readonly HashSet<string> identifiers = new();
        public override void Parse(){
            //new
            //parse all stuff before going into type-specific stuff
            
            current++;
            if(code[current].Type != TokenType.IDENTIFIER){throw new ExpaSyntaxError(code[current].Line,$"Expected identifier, got {code[current].Type}");}
            string identifier = code[current].Lexeme;
            if(identifiers.Contains(identifier)){
                throw new ExpaNameError(code[current].Line, $"Identifier cannot be used anywhere else(even outside of scope) {code[current].Lexeme}");
            }
            ArgumentDict? args = HasArgument()? new(): null;
            if(code[current + 1].Type == TokenType.RIGHTPAREN){
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
            switch(type){
                case TokenType.TEMPLATE:
                case TokenType.NATION:
                case TokenType.AREA:
                case TokenType.FUNCTION:
                    new ParseScope.ParseScope(Parser.UnparsedScopes(identifier, type));
                    break;
            }
        }
        private void Template(ArgumentDict? args, string identifier, string display, string? comment){
            if(args == null){
                //backtrack. If at any point it is unclear, throw an error.
                if(parent.children.Contains(identifier)){throw new ExpaReassignmentError(code[current].Line);}
                Parser.expaObjects[identifier] = new ExpaTemplate(parent, Parser.unparsedScopes![identifier], parent is ExpaNation? (ExpaNation)parent: GetNationParent(parent) ,false, display, comment);
                
            } else{
                try{
                    Parser.expaObjects[identifier] = new ExpaTemplate(parent, Parser.unparsedScopes![identifier], 
                            args.ContainsKey(TokenType.NATION)? //if nation is specified
                                (ExpaNation)Parser.expaObjects[args[TokenType.NATION].Lexeme]://pass the specified nation as an object by using the parent.children dictionary and the token from args
                            
                                    parent is ExpaNation ? (ExpaNation)parent: //elif immediate parent is an expanation
                                    GetNationParent(parent), // else try to get the parent
                         args.TryGetValue(TokenType.EQUALIZE, out var value) == true?//if equalize is specified
                            (value.Type == TokenType.TRUE?
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
                if(parent is not IMCanBeParent<ExpaNation> newParent){
                throw new ExpaArgumentError(code[current].Line, "Nation can only be a child of `global` or `nation` || Unable to determine exact error location, error occured around: ");
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
            IMCanBeParent<ExpaNation> lParent;
            try{
                 lParent = (IMCanBeParent<ExpaNation>)(args!.TryGetValue(TokenType.IPARENT, out Token? pValue)? Parser.expaObjects[pValue!.lexeme]:parent  );
            } catch(KeyNotFoundException){
                throw new ExpaSyntaxError(args[TokenType.IPARENT].Line, "Parent referenced has not been instantiated or is an invalid token");
            } catch(InvalidCastException){
                throw new ExpaArgumentError(code[current].Line, $"Error near definition of {identifier} - invalid parent, please explicitly define a valid immediate parent. possibly near");
            }

            Parser.expaObjects[identifier] = new ExpaNation(
                lParent,
                Parser.unparsedScopes[identifier],
                args.TryGetValue(TokenType.TIME, out Token? value)? //if there is specified time, use the specified time(goto immeidate purple brackets)
                /* #region */
                    (
                        value.Type == TokenType.NUMBER?//is it a number-like token? if yes, then parse it as a decimal AC time.
                        BackgroundObjects.BackgroundTime.ParseAcTime(value.literal!):
                        /* #region */
                        (
                            value.Type == TokenType.MONTHTIME?//if it is month-like, then parse it as a month-like token
                            BackgroundTime.ParseMonthTime(value.literal!)://if not,
                            /*#region */
                            (
                                /*#region*/
                                (
                                    value.Type == TokenType.IDENTIFIER//if it is an identifier - it could possibly be a time reference
                                    &&
                                    Parser.expaObjects.TryGetValue(value.lexeme, out IBaseObject? TValue)//and it actually is an identifier
                                    &&
                                    TValue is ExpaTime time//and the referenced object is a User-accessable time object
                                )?/*#endregion*/
                                time.Value://if so, return it as a Background-Time object
                                throw new ExpaArgumentError(value.line, "Invalid argument for `time`")//invalid because can't parse and isn't a identifier which is a time reference.
                            ) /*#endregion*/
                        )/*#endregion*/
                    ): /* #endregion */
                ((IHasTime)parent).Time, 
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
                    Parser.expaObjects[identifier] = new ExpaArea((IMCanBeParent<ExpaArea>)parent, (ExpaNation)parent, Defaults.MINCSS, Defaults.MAXCSS, Parser.unparsedScopes[identifier], display, comment);
                    return;
                } catch(InvalidCastException){
                    throw new ExpaArgumentError(code[current].Line, "Invalid parent or nation; please specify a valid parent as per the docs or specify a valid nation parent. Near");
                } catch(KeyNotFoundException){
                    throw new ExpaSyntaxError(code[current].Line, $"Could not find a defnition corresponding to the area {identifier}. Near");
                }
            }
            
            ExpaNation nation;//find nation parent
            try{
                nation = args!.TryGetValue(TokenType.NATION , out Token? nationIToken)? (ExpaNation)Parser.expaObjects[nationIToken.lexeme] : (parent is ExpaNation? (ExpaNation)parent: GetNationParent(parent)); // if nation specified, use it; else if parent is a nation then use that else trace. Catch invalid identifier.
            } catch(KeyNotFoundException){
                throw new ExpaArgumentError(args![TokenType.NATION].Line, "Invalid identifier");
            }
            IMCanBeParent<ExpaArea> parentV; //find normal parent
            try{
                parentV = args.TryGetValue(TokenType.IPARENT, out Token? IParentV)? (IMCanBeParent<ExpaArea>)Parser.expaObjects[IParentV.lexeme] : (IMCanBeParent<ExpaArea>)parent;
            } catch(KeyNotFoundException){
                throw new ExpaArgumentError(args[TokenType.IPARENT].Line, "Invalid identifier - please instantiate before usage");
            } catch(InvalidCastException){
                throw new ExpaArgumentError(code[current].Line, "No parent was specified and unable to implicitly specify a parent(parent namespace was not valid). Please specify a direct parent.Near ");
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
                throw new ExpaSyntaxError(code[current].Line, $"There was no definition for {identifier} found. Error occurred near");
            }
        }
        
        private void Function(ArgumentDict? args, string identifier, string display, string? comment){
            try{
                Parser.expaObjects[identifier] = new ExpaFunction((IMCanBeParent<ExpaFunction>)parent, Parser.unparsedScopes[identifier], display, comment);
                return;
            } catch(InvalidCastException){
                throw new ExpaArgumentError(code[current].Line, "Invalid parent or nation; please specify a valid parent as per the docs or specify a valid nation parent. Near:");
            } catch(KeyNotFoundException){
                throw new ExpaArgumentError(code[current].Line, $"Could not find a definition corresponding to the function {identifier}");
            }            
        }
        private static int IsValidSize(Token size){
            //if size is a number then parse and return else throw
            if(size.Type == TokenType.NUMBER){
                return int.Parse(size.Literal!);
            } else{
                throw new ExpaArgumentError(size.Line, $"Invalid argument for maxsize {size}(type {size.Type}, expected number)");
                }
        }
        private ArgumentDict ExtractArguments(ArgumentDict args){
            List<Token[]> argList = new();
            current++;
            start = current;
            TokenType i;
            while(code[current].Type!= TokenType.RIGHTPAREN){
                if(code[current].Type == TokenType.SEMICOLON){
                    argList.Add(code[start..current]);//no +1 on the current to skip the semicolon
                    start = current;
                }
                current++;
            }
            foreach(Token[] argLine in argList){
                if(argLine.Any(x => x.Type == TokenType.COLON)){
                    args[Keywords.IsValidArgumentName(i = argLine[0].Type)? i: throw new ExpaArgumentError(code[current].Line,$"Invalid argument type {code[current].Lexeme}")] = argLine[2];
                    //if there is a kwarg, but the kw is not valid, throw.
                } else{
                    args[(i = Converters.IntToTType(++current)) == TokenType.INTERPRETERNULL? throw new ExpaArgumentError(code[current].Line, $"Max positional argument count exceeded(max: {current-1})"): i] = argLine[0];
                    //if the conversion returns InterpreterNull, that means that the conversion failed, and thus also means that the positional argument limit was exceeded.
                }
            }
            return args;
        }
        private ExpaNation GetNationParent(IBaseObject input){
            //input target object
            ExpaNation? nation = null;
            ExpaNation? tempNation = null;
            //where there is a nation in the lineage, that is the nation. else, we keep going
            if(parent.parents.Contains("global")){throw new ExpaSyntaxError(code[current].Line, $"Unable to backtrace valid nation origin, please explicitly declare the nation");}
            IBaseObject value;
            foreach(string sv in input.parents){
                value = Parser.expaObjects[sv];
                if(value is ExpaNation){
                    if(nation == null){
                        nation = (ExpaNation)value;
                    } else{
                        nation = null;
                        throw new ExpaSyntaxError(code[current].Line, $"Unable to backtrace valid nation origin, please explicitly declare the nation");
                    }
                } else{
                    if(tempNation == null){
                        //if there is no direct nation parent yet, set tempnation to parent(if there is no valid parent down that path then error is thrown there)
                        tempNation = GetNationParent(value);
                    } else if(tempNation != GetNationParent(value)){//if we have conflicting nation parents
                        throw new ExpaSyntaxError(code[current].Line, $"Unable to backtrace valid nation origin, please explicitly declare the nation");
                    } 
                }
            }
            //return the non-null value; if both are null then throw
            return nation ?? tempNation ??  throw new ExpaSyntaxError(code[current].Line, $"Unable to backtrace valid nation origin, please explicitly declare the nation");
        }
    }
}