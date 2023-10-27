namespace Commands{
    using Tokens;
    using Structs;
    using Errors;
    using ExpaObjects;
    abstract public class Commands{
        public int current;
        public Token[] code;
        public ExpaNameSpace parent;
        public parser.Parser parser;
        public ExpaNameSpace self;
        public Commands(CodeParseTransferrer input, parser.Parser parser){
            current = input.current;
            code = input.tokenList;
            parent = input.parent;
            this.parser = parser;
            this.self = input.self;
        }
        public CodeParseTransferrer Transfer(){
            return new(current, code, parent, self);
        }
        public abstract void Parse();
        public bool HasArgument(){
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