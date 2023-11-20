using Tokens;

namespace Commands{
    internal class ForEach : Command
    {
        private Token[] LoopControl{ get; init; }
        public ForEach(Token[] loopControl, Token[] codeSection, string parentStringID) : base(codeSection, parentStringID)
        {
            LoopControl = loopControl;
        }

        internal override void Execute()
        {
            throw new NotImplementedException();
        }
    }
}