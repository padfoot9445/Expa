using Tokens;

namespace Commands{
    internal class Permanent : Command
    {
        public Permanent(Token[] codeSection, string parentStringID) : base(codeSection, parentStringID)
        {
        }

        internal override void Execute()
        {
            throw new NotImplementedException();
        }
    }
}