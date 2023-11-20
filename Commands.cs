using Tokens;
using Structs;
using Errors;
using Interfaces;

namespace Commands
{
    internal interface ICommand{
        internal void Execute();
    }
    internal abstract class BaseCommand: ICommand{
        
        private string ParentStrID { get; init; }
        private Token[] CodeSection { get; init; }
        internal BaseCommand(Token[] codeSection, string parentStringID){
            CodeSection = codeSection;
            ParentStrID = parentStringID;
        }
        public abstract void Execute();
    }
    

}