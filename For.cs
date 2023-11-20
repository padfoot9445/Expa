using Tokens;

namespace Commands{
    internal class For : Command
    {
        public For(Token[] codeSection, string parentStringID) : base(codeSection, parentStringID)
        {
        }

        internal override void Execute()
        {
            throw new NotImplementedException();
        }
    }
}