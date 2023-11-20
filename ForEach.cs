using Tokens;

namespace Commands{
    internal class ForEach : BaseCommand
    {
        private Token[] LoopControl{ get; init; }
        public ForEach(Token[] loopControl, Token[] codeSection, string parentStringID) : base(codeSection, parentStringID)
        {
            LoopControl = loopControl;
        }

        public override void Execute()
        {
            throw new NotImplementedException();
        }
    }
}