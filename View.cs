using Tokens;

namespace Commands{
    internal class View : BaseCommand
    {
        public View(Token[] codeSection, string parentStringID) : base(codeSection, parentStringID)
        {
        }

        public override void Execute()
        {
            throw new NotImplementedException();
        }
    }
}