using Tokens;

namespace Commands.Ctrl;
internal class ExpaIf : Command
{
    private Token[] Condition{ get; init; }
    public ExpaIf(Token[] condition, Token[] codeSection, string parentStringID) : base(codeSection, parentStringID)
    {
        Condition = condition;
    }

    internal override void Execute()
    {
        throw new NotImplementedException();
    }
}