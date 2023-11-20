using Tokens;

namespace Commands{
    internal class For : BaseCommand
    {
        private Token[] Condition{ get; init; }
        public For(Token[] condition, Token[] codeSection, string parentStringID) : base(codeSection, parentStringID)
        {
            Condition = condition;
        }

        public override void Execute()
        {
            throw new NotImplementedException();
        }
    }
}