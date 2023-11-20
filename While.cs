using Tokens;

namespace Commands{
    internal class While : BaseCommand
    {
        private Token[] LoopControl { get; init; }
        internal While(Token[] LoopControl, Token[] codeSection, string parentStringID) : base(codeSection, parentStringID)
        {
            this.LoopControl = LoopControl;
        }

        public override void Execute()
        {
            throw new NotImplementedException();
        }
    }
}