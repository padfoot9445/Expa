using Tokens;

namespace Commands.Ctrl{
    internal class ExpaSwitch : Command
    {
        public ExpaSwitch(Token[] codeSection, string parentStringID) : base(codeSection, parentStringID)
        {
        }

        internal override void Execute()
        {
            throw new NotImplementedException();
        }
    }
}