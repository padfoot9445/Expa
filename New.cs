using Tokens;

namespace Commands{
    internal class New : Command{
        public New(Token[] codeSection, string parentStringID) : base(codeSection, parentStringID){
        }

        internal override void Execute()
        {
            throw new NotImplementedException();
        }
    }
}