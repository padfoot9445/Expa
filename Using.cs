using Tokens;

namespace Commands{
    internal class Using(Token[] codeSection, string parentStringID) : BaseCommand(codeSection, parentStringID)
    {
        public override void Execute()
        {
            throw new NotImplementedException();
        }
    }
}