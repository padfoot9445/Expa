
namespace BackgroundObjects
{
    using Interfaces;
    using Structs;
    using Markers;
    using Tokens;
    using Constants;

    internal class StaticCommand{
        public void Execute(){
            
        }
        public StaticCommand(){
            throw new NotImplementedException();
        }
    }
    internal class ArgumentList: List<Argument>{
        public new void Add(Argument input)
        {
            base.Add(input);
            this.Sort((x, y) => x.Position.CompareTo(y.Position));
        }
    }
    internal abstract class BaseExpaObject: IMExpaRelatedObject, IExpaObject{
        public abstract TokenType Type{ get; }
        public virtual bool IsNameSpace => false;
        public string Display{ get; set; }
        public string? Comment{ get; set; }
        public string StringIdentifier{ get; init; }
        public virtual string StringID =>StringIdentifier;
        protected BaseExpaObject(string stringIdentifier, string display, string? comment){
            StringIdentifier = stringIdentifier;
            this.Display = display;
            this.Comment = comment;
        }
    }
    internal abstract class BaseExpaNonGlobalObject: BaseExpaObject, IHasStringID, IExpaNonGlobalObject{
        protected BaseExpaNonGlobalObject(string parentStringID, string stringIdentifier, string? display=null, string? comment=null) : base(stringIdentifier, display is null? stringIdentifier : display, comment){
            
            this.StringIdentifier = stringIdentifier;
            this.ParentStringID = parentStringID;
        }
        public string ParentStringID{ get; set; }
        public override string StringID => ParentStringID + ExpaObjectConstants.OBJECT_ID_SEPERATOR + StringIdentifier;
    }
    internal abstract class BaseExpaNameSpace: BaseExpaNonGlobalObject, IExpaNameSpace{
        public override bool IsNameSpace => true;
        public List<string> ChildrenStringIDs{ get; init; }
        public Scope Scope{ get; init; }
        protected BaseExpaNameSpace(string parentStringID, string stringIdentifier, Scope scope, List<string> childrenStringIDs, string? display, string? comment) : base(parentStringID, stringIdentifier, display, comment){
            this.ChildrenStringIDs = childrenStringIDs;
            Scope = scope;
        }

    }
}