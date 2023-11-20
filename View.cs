using Tokens;

namespace Commands{
    internal class View : Command
    {
        public View(Token[] codeSection, string parentStringID) : base(codeSection, parentStringID)
        {
        }

        internal override void Execute()
        {
            throw new NotImplementedException();
        }
    }
}