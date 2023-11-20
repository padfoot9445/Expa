using Tokens;

namespace Commands{
    internal class While : Command
    {
        private Token[] LoopControl { get; init; }
        internal While(Token[] LoopControl, Token[] codeSection, string parentStringID) : base(codeSection, parentStringID)
        {
            this.LoopControl = LoopControl;
        }

        internal override void Execute()
        {
            throw new NotImplementedException();
        }
    }
}