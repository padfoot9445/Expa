using Tokens;
using Structs;
using Errors;
using Interfaces;

namespace Commands
{
    abstract public class Commands{
        public int start;
        public int current;
        public int argNum = 0;
        public Token[] code;
        public BaseExpaNameSpace parent;
        public Commands(CodeParseTransferrer input){
            current = input.current;
            start = current;
            code = input.tokenList;
            parent = input.parent;
        }
        public CodeParseTransferrer Transfer(){
            return new(current, code, parent);
        }
        public abstract void Parse();
        public bool HasArgument(){
            current++;
            if(code[current].Type == TokenType.LEFTPAREN){
                return true;
            } else if(code[current].Type == TokenType.SEMICOLON){
                return false;
            } else{
                throw new ExpaSyntaxError(code[current].Line, $"Expected parenthesis or semicolon, got {code[current].Type}");
            }
        }
        public int Increment() => current - start;
    }
    

}