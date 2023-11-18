
namespace BackgroundObjects
{
    using Interfaces;
    using Structs;
    using Markers;
    using Tokens;
    public class StaticCommands{
        public void Execute(){
            
        }
        public StaticCommands(){
            throw new NotImplementedException();
        }
    }
    public class ArgumentList: List<Argument>{
        public new void Add(Argument input)
        {
            base.Add(input);
            this.Sort((x, y) => x.Position.CompareTo(y.Position));
        }
    }
    public abstract class BaseExpaObject: IMExpaRelatedObject{
        public abstract TokenType Type{ get; }
        public virtual bool IsNameSpace => false;
        public string Display{ get; internal set; }
        public string? Comment{ get; internal set; }
        protected BaseExpaObject(string display, string? comment){
            this.Display = display;
            this.Comment = comment;
        }
    }
    public abstract class BaseExpaNonGlobalObject: BaseExpaObject{
        protected BaseExpaNonGlobalObject(string parentStringID, string stringIdentifier, string? display=null, string? comment=null) : base(display is null? stringIdentifier : display, comment){
            this.StringIdentifier = stringIdentifier;
            this.ParentStringID = parentStringID;
        }
        public string ParentStringID{ get; internal set; }
        public string StringIdentifier{ get; init; }
        public string StringID => ParentStringID + Constants.ExpaObjectConstants.OBJECTIDSEPERATOR + StringIdentifier;
    }
    public abstract class BaseExpaNameSpace: BaseExpaNonGlobalObject, IExpaNameSpace{
        public override bool IsNameSpace => true;
        public List<string> ChildrenStringIDs{ get;}
        public Scope Scope{ get; }
        protected BaseExpaNameSpace(string parentStringID, string stringIdentifier, string display, string comment, List<string> childrenStringIDs) : base(parentStringID, stringIdentifier, display, comment){
            this.ChildrenStringIDs = childrenStringIDs;
        }
        protected BaseExpaNameSpace(string parentStringID, string stringIdentifier, string display, string comment, string[] childrenStringIDs) : base(parentStringID, stringIdentifier, display, comment){
            this.ChildrenStringIDs = childrenStringIDs.ToList();
        }

    }
}