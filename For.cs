using Tokens;

namespace Commands{
    internal class For : Command
    {
        private Token[] Condition{ get; init; }
        public For(Token[] condition, Token[] codeSection, string parentStringID) : base(codeSection, parentStringID)
        {
            Condition = condition;
        }

        internal override void Execute()
        {
            throw new NotImplementedException();
        }
    }
}