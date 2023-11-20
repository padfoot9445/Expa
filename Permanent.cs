using Tokens;

namespace Commands{
    internal class Permanent : BaseCommand
    {
        public Permanent(Token[] codeSection, string parentStringID) : base(codeSection, parentStringID)
        {
        }

        public override void Execute()
        {
            throw new NotImplementedException();
        }
    }
}