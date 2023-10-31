namespace Commands{
    using Tokens;
    using Structs;
    using Errors;
    using ExpaObjects;
    abstract public class Commands{
        public int current;
        public Token[] code;
        public ExpaNameSpace parent;
        public Commands(CodeParseTransferrer input){
            current = input.current;
            code = input.tokenList;
            parent = input.parent;
        }
        public CodeParseTransferrer Transfer(){
            return new(current, code, parent);
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