using Tokens;

namespace Commands{
    internal class While : Command
    {
        public While(Token[] codeSection, string parentStringID) : base(codeSection, parentStringID)
        {
        }

        internal override void Execute()
        {
            throw new NotImplementedException();
        }
    }
}