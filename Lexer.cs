namespace lexer{
    using Tokens;
    class Lexer{
        public string code;
        private int current = 0;
        private int start = 0;
        private int line = 1;
        private readonly List<Token> tokenList = new List<Token>();
        public Dictionary<string, TokenType> keywords = Keywords.keywords;
        public Lexer(string Acode){
            code = Acode;
        }			
        public Token[] getTokens(){
            while(!isAtEnd()){;
                switch(code[current]){
                    case '(':
                        tokenList.Add(new Token(TokenType.LEFTPAREN, line, "(", null));
                        current++;
                        start = current;
                        break;
                    case ')':
                        tokenList.Add(new Token(TokenType.RIGHTPAREN, line, ")", null));
                        current++;
                        start = current;
                        break;
                    case '{':
                        tokenList.Add(new Token(TokenType.LEFTBRACE, line, "{", null));
                        current++;
                        start = current;
                        break;
                    case '}':
                        tokenList.Add(new Token(TokenType.RIGHTBRACE, line, "}", null));
                        current++;
                        start = current;
                        break;
                    case ';':
                        tokenList.Add(new Token(TokenType.SEMICOLON, line, ";", null));
                        current++;
                        start = current;
                        break;
                    case '.':
                        tokenList.Add(new Token(TokenType.DOT, line, ".", null));
                        current++;
                        start = current;
                        break;
                    case ':':
                        tokenList.Add(new Token(TokenType.COLON, line, ":", null));
                        current++;
                        start = current;
                        break;
                    case ' ':
                    case '\t':
                        current++;
                        start = current;
                        break;
                    case '\n':
                        current++;
                        start = current;
                        line++;
                        break;
                    case '+':
                        current++;
                        start = current;
                        tokenList.Add(new Token(TokenType.PLUS, line, "+", null));
                        break;
                    case '-':
                        current++;
                        start = current;
                        tokenList.Add(new Token(TokenType.MINUS, line, "-", null));
                        break;
                    case '*':
                        current++;
                        start = current;
                        tokenList.Add(new Token(TokenType.STAR, line, "*", null));
                        break;
                    case '/':
                        current++;
                        if(code[current] == '/'){
                            while(code[current] != '\n'){
                                current++;
                            }
                            current++;
                            line++;
                        } else{
                            tokenList.Add(new Token(TokenType.SLASH, line, "/", null));
                        }
                        start = current;
                        break;
                    case '"':
                        current++;
                        start = current;
                        while(code[current] != '"'){
                            current++;
                            if(code[current] == '\n'){
                                line++;
                            }
                        }
                        tokenList.Add(new Token(TokenType.STRING, line, code.Substring(start,current), code.Substring(start,current)));
                        break;
                    case '%':
                        current++;
                        start = current;
                        tokenList.Add(new Token(TokenType.PERCENT, line, "%", null));
                        break;
                    case '=':
                        current++;
                        start = current;
                        if(code[current] == '='){
                            tokenList.Add(new Token(TokenType.DOUBLEEQUALS, line, "==", null));
                        } else{
                            tokenList.Add(new Token(TokenType.EQUALS, line, "=", null));
                        }
                        break;
                    default:
                        start = current;
                        //numbers
                        if(Char.IsDigit(code[current])){
                            while(!isAtEnd() && (Char.IsDigit(code[current]) || code[current] == '.' || code[current] == ' ' || code[current] == ',' || code[current] == '-')){
                                current++;
                            }
                            tokenList.Add(new Token(TokenType.NUMBER, line, code.Substring(start, current), code.Substring(start,current)));
                            break;
                        } else if (Char.IsLetter(code[current]) || code[current] == '_'){
                            //identifiers and keywords
                            while(!isAtEnd() && code[current] != ' ' && code[current] != '\t' && code[current] != '\n' && char.IsLetterOrDigit(code[current])){
                                current++;
                            }
                            if(keywords.ContainsKey(code.Substring(start, current))){
                                tokenList.Add(new Token(keywords[code.Substring(start,current)], line, code.Substring(start, current), null));
                            } else{                            
                                tokenList.Add(new Token(TokenType.IDENTIFIER, line, code.Substring(start, current), code.Substring(start, current)));
                            }
                        }
                        //TODO: throw exception;
                        break;
                        
                        
                        
                }
                
            }
            return tokenList.ToArray();
        }
        private bool isAtEnd(){
            if(current >= code.Length){
                return true;
            } else{
                return false;
            }
        }
    }
}