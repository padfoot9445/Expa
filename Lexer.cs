using Errors;
using Tokens;
using Constants;
using Helpers;

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
                        tokenList.Add(new(TokenType.LEFTSQUAREBRACKET, TokenTypeType.SYMBOL, line, LexerConstants.Literals.LEFTSQUAREBRACKET, null));
                        current++;
                        start = current;
                        break;
                    case LexerConstants.Chars.RIGHTSQUAREBRACKET:
                        tokenList.Add(new(TokenType.RIGHTSQUAREBRACKET, TokenTypeType.SYMBOL, line, LexerConstants.Literals.RIGHTSQUAREBRACKET, null));
                        current++;
                        start = current;
                        break;
                    case LexerConstants.Chars.COMMA:
                        tokenList.Add(new(TokenType.COMMA, TokenTypeType.SYMBOL, line, LexerConstants.Literals.COMMA, null));
                        current++;
                        start = current;
                        break;
                    case LexerConstants.Chars.LEFTPAREN:
                        tokenList.Add(new Token(TokenType.LEFTPAREN, TokenTypeType.SYMBOL, line, LexerConstants.Literals.LEFTPAREN, null));
                        current++;
                        start = current;
                        break;
                    case LexerConstants.Chars.RIGHTPAREN:
                        tokenList.Add(new Token(TokenType.RIGHTPAREN, TokenTypeType.SYMBOL, line, LexerConstants.Literals.RIGHTPAREN, null));
                        current++;
                        start = current;
                        break;
                    case LexerConstants.Chars.LEFTBRACE:
                        tokenList.Add(new Token(TokenType.LEFTBRACE, TokenTypeType.SYMBOL, line, LexerConstants.Literals.LEFTBRACE, null));
                        current++;
                        start = current;
                        break;
                    case LexerConstants.Chars.RIGHTBRACE:
                        tokenList.Add(new Token(TokenType.RIGHTBRACE, TokenTypeType.SYMBOL, line, LexerConstants.Literals.RIGHTBRACE, null));
                        current++;
                        start = current;
                        break;
                    case LexerConstants.Chars.SEMICOLON:
                        tokenList.Add(new Token(TokenType.SEMICOLON, TokenTypeType.SYMBOL, line, LexerConstants.Literals.SEMICOLON, null));
                        current++;
                        start = current;
                        break;
                    case LexerConstants.Chars.DOT:
                        tokenList.Add(new Token(TokenType.DOT, TokenTypeType.SYMBOL, line, LexerConstants.Literals.DOT, null));
                        current++;
                        start = current;
                        break;
                    case LexerConstants.Chars.COLON:
                        tokenList.Add(new Token(TokenType.COLON, TokenTypeType.SYMBOL, line, LexerConstants.Literals.COLON, null));
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
                        current++;
                        start = current;
                        if(code[current] == LexerConstants.Chars.PLUS){
                            tokenList.Add(new Token(TokenType.DOUBLE_PLUS, TokenTypeType.COMMAND, line, LexerConstants.Literals.DOUBLE_PLUS, null));
                            break;
                        }
                        tokenList.Add(new Token(TokenType.PLUS, TokenTypeType.OPERATOR, line, LexerConstants.Literals.PLUS, null));
                        break;
                    case LexerConstants.Chars.MINUS:
                        current++;
                        start = current;
                        if(code[current] == LexerConstants.Chars.MINUS){
                            tokenList.Add(new Token(TokenType.DOUBLE_MINUS, TokenTypeType.COMMAND, line, LexerConstants.Literals.DOUBLE_MINUS, null));
                            break;
                        }
                        tokenList.Add(new Token(TokenType.MINUS, TokenTypeType.OPERATOR, line, LexerConstants.Literals.MINUS, null));
                        break;
                    case LexerConstants.Chars.STAR:
                        current++;
                        start = current;
                        if(code[current] == LexerConstants.Chars.STAR){
                            tokenList.Add(new Token(TokenType.DOUBLE_STAR, TokenTypeType.OPERATOR, line, LexerConstants.Literals.DOUBLE_STAR, null));
                            break;
                        }
                        tokenList.Add(new Token(TokenType.STAR, TokenTypeType.OPERATOR, line, LexerConstants.Literals.STAR, null));
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
                            tokenList.Add(new Token(TokenType.SLASH, TokenTypeType.OPERATOR, line, LexerConstants.Literals.SLASH, null));
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
                        tokenList.Add(new Token(TokenType.STRING, TokenTypeType.VALUE, line, code[start..(current - 1)], code[start..(current - 1)]));
                        break;
                    case LexerConstants.Chars.PERCENT:
                        tokenList.Add(new Token(TokenType.PERCENT, TokenTypeType.OPERATOR, line, LexerConstants.Literals.PERCENT, null));
                        current++;
                        start = current;
                        break;
                    case LexerConstants.Chars.EQUAL_SIGN:
                        current++;
                        start = current;
                        if(code[current] == LexerConstants.Chars.EQUAL_SIGN){
                            tokenList.Add(new Token(TokenType.DOUBLEEQUALS, TokenTypeType.CTRL_OPERATOR, line, LexerConstants.Literals.DOUBLEEQUALS, null));
                        } else{
                            tokenList.Add(new Token(TokenType.EQUALS, TokenTypeType.COMMAND, line, LexerConstants.Literals.EQUALS, null));
                        }
                        break;
                    default:
                        start = current;
                        //numbers
                        if(Char.IsDigit(code[current])){
                            while(!isAtEnd() && IsValid.NumberOrMonthChar(code[current])){
                                current++;
                            }
                            string extract = code[start..current];
                            tokenList.Add(
                                extract.Contains(LexerConstants.Chars.SLASH)?
                                new Token(TokenType.MONTHTIME, TokenTypeType.VALUE, line, extract, extract):
                                new Token(TokenType.NUMBER, TokenTypeType.VALUE, line, extract, extract)
                            );
                            if(extract.Count(c=>c==LexerConstants.Chars.DOT) > 1){
                                throw new ExpaSyntaxError(line, "Number must not contain more than one decimal point");
                            } else if(extract.Count(c=>c==LexerConstants.Chars.SLASH) > 1){
                                throw new ExpaSyntaxError(line, "MonthTime must not contain more than one slash");
                            }
                            break;
                        } else if (IsValid.IdentifierStartChar(code[current])){
                            //identifiers and keywords
                            while(!isAtEnd() && IsValid.IdentifierChar(code[current])){
                                current++;
                            }
                            string currentWord = code[start..current];
                            if(Keywords.StrKWToTType(currentWord) != TokenType.INTERPRETERNULL){
                                tokenList.Add(
                                    new Token(
                                        Keywords.StrKWToTType(currentWord),
                                        Keywords.IdentifierToTTypeType(currentWord),
                                        line,
                                        currentWord,
                                        null
                                    )
                                );
                            } else{                            
                                tokenList.Add(
                                    new Token(
                                        TokenType.IDENTIFIER, 
                                        TokenTypeType.IDENTIFIER,
                                        line, 
                                        currentWord, 
                                        currentWord
                                    )
                                );
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