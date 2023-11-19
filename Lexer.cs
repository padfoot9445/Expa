using Errors;
using Tokens;
using Constants;

namespace lexer{
    class Lexer{
        public string code;
        private int current = 0;
        private int start = 0;
        private int line = 1;
        private readonly List<Token> tokenList = new();
        public Lexer(string Acode){
            code = Acode;
        }			
        public Token[] GetTokens(){
            while(!isAtEnd()){
                switch(code[current]){
                    case LexerConstants.Chars.LEFTSQUAREBRACKET:
                        tokenList.Add(new(TokenType.LEFTSQUAREBRACKET, line, LexerConstants.Literals.LEFTSQUAREBRACKET, null));
                        current++;
                        start = current;
                        break;
                    case LexerConstants.Chars.RIGHTSQUAREBRACKET:
                        tokenList.Add(new(TokenType.RIGHTSQUAREBRACKET, line, LexerConstants.Literals.RIGHTSQUAREBRACKET, null));
                        current++;
                        start = current;
                        break;
 
                    case LexerConstants.Chars.LEFTPAREN:
                        tokenList.Add(new Token(TokenType.LEFTPAREN, line, LexerConstants.Literals.LEFTPAREN, null));
                        current++;
                        start = current;
                        break;
                    case LexerConstants.Chars.RIGHTPAREN:
                        tokenList.Add(new Token(TokenType.RIGHTPAREN, line, LexerConstants.Literals.RIGHTPAREN, null));
                        current++;
                        start = current;
                        break;
                    case LexerConstants.Chars.LEFTBRACE:
                        tokenList.Add(new Token(TokenType.LEFTBRACE, line, LexerConstants.Literals.LEFTBRACE, null));
                        current++;
                        start = current;
                        break;
                    case LexerConstants.Chars.RIGHTBRACE:
                        tokenList.Add(new Token(TokenType.RIGHTBRACE, line, LexerConstants.Literals.RIGHTBRACE, null));
                        current++;
                        start = current;
                        break;
                    case LexerConstants.Chars.SEMICOLON:
                        tokenList.Add(new Token(TokenType.SEMICOLON, line, LexerConstants.Literals.SEMICOLON, null));
                        current++;
                        start = current;
                        break;
                    case LexerConstants.Chars.DOT:
                        tokenList.Add(new Token(TokenType.DOT, line, LexerConstants.Literals.DOT, null));
                        current++;
                        start = current;
                        break;
                    case LexerConstants.Chars.COLON:
                        tokenList.Add(new Token(TokenType.COLON, line, LexerConstants.Literals.COLON, null));
                        current++;
                        start = current;
                        break;
                    case LexerConstants.Chars.SPACE:
                    case LexerConstants.Chars.RETURN:
                    case LexerConstants.Chars.TAB:
                        current++;
                        start = current;
                        break;
                    case LexerConstants.Chars.NEWLINE:
                        current++;
                        start = current;
                        line++;
                        break;
                    case LexerConstants.Chars.PLUS:
                        tokenList.Add(new Token(TokenType.PLUS, line, LexerConstants.Literals.PLUS, null));
                        current++;
                        start = current;
                        break;
                    case LexerConstants.Chars.MINUS:
                        tokenList.Add(new Token(TokenType.MINUS, line, LexerConstants.Literals.MINUS, null));
                        current++;
                        start = current;
                        break;
                    case LexerConstants.Chars.STAR:
                        tokenList.Add(new Token(TokenType.STAR, line, LexerConstants.Literals.STAR, null));
                        current++;
                        start = current;
                        break;
                    case LexerConstants.Chars.SLASH:
                        current++;
                        if(code[current] == LexerConstants.Chars.SLASH){
                            while(code[current] != LexerConstants.Chars.NEWLINE){
                                current++;
                            }
                            current++;
                            line++;
                        } else{
                            tokenList.Add(new Token(TokenType.SLASH, line, LexerConstants.Literals.SLASH, null));
                        }
                        start = current;
                        break;
                    case LexerConstants.Chars.DOUBLE_QUOTE:
                        current++;
                        start = current;
                        while(code[current] != LexerConstants.Chars.DOUBLE_QUOTE){
                            current++;
                            if(code[current] == LexerConstants.Chars.NEWLINE){
                                line++;
                            }
                        }
                        current++;
                        tokenList.Add(new Token(TokenType.STRING, line, code[start..(current - 1)], code[start..(current - 1)]));
                        break;
                    case LexerConstants.Chars.PERCENT:
                        tokenList.Add(new Token(TokenType.PERCENT, line, LexerConstants.Literals.PERCENT, null));
                        current++;
                        start = current;
                        break;
                    case LexerConstants.Chars.EQUAL_SIGN:
                        current++;
                        start = current;
                        if(code[current] == LexerConstants.Chars.EQUAL_SIGN){
                            tokenList.Add(new Token(TokenType.DOUBLEEQUALS, line, LexerConstants.Literals.DOUBLEEQUALS, null));
                        } else{
                            tokenList.Add(new Token(TokenType.EQUALS, line, LexerConstants.Literals.EQUALS, null));
                        }
                        break;
                    default:
                        start = current;
                        //numbers
                        if(Char.IsDigit(code[current])){
                            while(!isAtEnd() && LexerConstants.IsValid.NumberOrMonthChar(code[current])){
                                current++;
                            }
                            string extract = code[start..current];
                            tokenList.Add(
                                extract.Contains('/')?
                                new Token(TokenType.MONTHTIME, line, extract, extract):
                                new Token(TokenType.NUMBER, line, extract, extract)
                            );
                            if(extract.Count(c=>c=='.') > 1){
                                throw new ExpaSyntaxError(line, "Number must not contain more than one decimal point");
                            } else if(extract.Count(c=>c=='/') > 1){
                                throw new ExpaSyntaxError(line, "Month must not contain more than one slash");
                            }
                            break;
                        } else if (Char.IsLetter(code[current]) || code[current] == '_'){
                            //identifiers and keywords
                            while(!isAtEnd() && code[current] != ' ' && code[current] != '\t' && code[current] != LexerConstants.Chars.NEWLINE && char.IsLetterOrDigit(code[current])){
                                current++;
                            }
                            if(Keywords.KeyWords(code[start..current]) != TokenType.INTERPRETERNULL){
                                tokenList.Add(new Token(Keywords.KeyWords(code[start..current]), line, code[start..current], null));
                            } else{                            
                                tokenList.Add(new Token(TokenType.IDENTIFIER, line, code[start..current], code[start..current]));
                            }
                        } else{
                            current++;
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