using Tokens;

namespace Commands.Ctrl;
internal class ExpaIf : ICommand
{
    private ExpaIfThen[] ifThen{ get; init; }
    private string ParentStrID{ get; init; }
    public ExpaIf(ExpaIfThen[] ifThen, string parentStringID) 
    {
        this.ifThen = ifThen;
        ParentStrID = parentStringID;
    }

    public void Execute()
    {
        throw new NotImplementedException();
    }
    
}
internal readonly record struct ExpaIfThen(Token[]? Condition, Token[] Then);