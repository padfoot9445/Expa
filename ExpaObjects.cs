using Errors;
using Tokens;

namespace ExpaObjects{
    using Tokens;
    using Structs;
    using BackgroundObjects;
    using System.Data.Common;
    using System.Runtime.Serialization;
    using Helpers;
    using Parser;
    using Metadata;
    using Interfaces;
    //*Namespaces
    /*#region Namespaces*/
    public class ExpaGlobal : BaseNameSpace, IHasTime, ICanBeParent<ExpaNation>{
        public BackgroundTime Time{get; private set;}
        public ExpaGlobal(Scope scope, string? display=null, string? comment = null): base(scope, display, comment){
            Time = new(0, 0);
        }
        public ExpaGlobal(Scope scope, BackgroundTime time,string? display=null, string? comment = null): base(scope, display, comment){
            Time = time;
        }
        public ExpaGlobal(Scope scope, BackgroundTime time): base(scope){
            this.Time = time;
        }
    }
    public class ExpaNation: BaseNameSpace, IHasTime, ICanBeParent<ExpaNation>, ICanBeParent<ExpaArea>, ICanBeParent<ExpaFunction>, IHasMinMaxSize{
        public BackgroundTime Time{get; private set;}
        public int MaxChildShipSize{get; set;}
        public int MinChildShipSize{get; set;}

        public ExpaNation(ICanBeParent<ExpaNation> parent, Scope scope, BackgroundTime time, int MinChildShipSize=Helpers.Defaults.MINCSS, int MaxChildShipSize=Defaults.MAXCSS,string? display=null, string? comment = null):base((BaseNameSpace)parent, scope, display, comment){//handle implicit time on caller side; Global gets its time by modify and not set.
            this.Time = time;
            this.MinChildShipSize = MinChildShipSize;
            this.MaxChildShipSize = MaxChildShipSize;
        }
    }
    public class ExpaArea : BaseNameSpace, ICanBeParent<ExpaArea>,ICanBeParent<ExpaFunction>, IHasMinMaxSize{
        public ExpaNation mainNationParent;
        public int MinChildShipSize{get; set;}
        public int MaxChildShipSize{get; set;}
        public ExpaArea(ICanBeParent<ExpaArea> parent, ExpaNation mainNationParent, int minCSS, int maxCSS,Scope aScope, string? display = null, string? comment = null) : base((BaseNameSpace)parent, aScope, display, comment){
            this.mainNationParent = mainNationParent;
            this.MinChildShipSize = minCSS;
            this.MaxChildShipSize = maxCSS;
        }
        
    }
    public class ExpaFunction: BaseNameSpace, IReusable, ICanBeParent<ExpaFunction>{
        private StaticCommands[] privateCommands = Array.Empty<StaticCommands>();
        public StaticCommands[] commands{get => privateCommands; set{privateCommands = value;}}
        
        public ExpaFunction(ICanBeParent<ExpaFunction> parent, Scope scope, string? display = null, string? comment = null) : base((BaseNameSpace)parent, scope, display, comment){
        }
        public void Reuse(){
            
        }
        
    }
    public class ExpaTemplate: BaseNameSpace, IReusable{
        public ExpaNation nation;
        public bool equalize;
        static readonly HashSet<TokenType> allowedKw = new(){
            TokenType.EQUALIZE,
            TokenType.NATION
        };

        public ExpaTemplate(BaseNameSpace parent, Scope scope, ExpaNation nation, bool equalize, string? display=null, string? comment = null): base(parent, scope, display, comment){
            this.nation = nation;
            this.equalize = equalize;
        }
        public void Reuse(){}
    }
    /*#endregion*/
    //*Values
    /*#region Values*/
    public class ExpaTime: BaseObject,IHasTime{
        public BackgroundTime Time{get; private set;}
        public ExpaTime(BaseNameSpace parent, Token identifier, BackgroundTime time, string? display = null, string? comment = null) : base(parent, identifier, display, comment){
            Time = time;
        }
    }
    
    public class ExpaNumber : BaseObject{
        public readonly string value;
        public readonly int wholeNumber;
        public readonly int decimalNumber;
        //<summary>Value should be in format [0-9]*.[0-9]* or [0-9]*</summary>
        public ExpaNumber(BaseNameSpace parent,string value, Token identifier, string? display = null, string? comment = null) : base(parent, identifier, display, comment){
            this.value = value;
            string[] parts = value.Split('.');
            if(parts.Length > 2){
                throw new ExpaInterpreterError(-1, "Invalid number passed to number class - authenticate before construction.");
            }
        }

    }
    /*#endregion*/
    //*Constructables
    /*#region*/
    public class ExpaShipClass : BaseObject, IConstructable{
        public int Count{get; set;}
        public BackgroundTime Duration{get; private set;}
        public ExpaShipClass(BaseNameSpace parent, Token identifier, BackgroundTime duration, string? display = null, string? comment = null) : base(parent, identifier, display, comment){
            this.Duration = duration;
            this.Count = 0;
        }
    }
    public class ExpaComponent : BaseObject, IFastConstructable{
        public int Amount{get; private set;}

        public int Count {get; set;}

        public BackgroundTime Duration{get; private set;}
        public ExpaComponent(BaseNameSpace parent, Token identifier, BackgroundTime duration, int amount, string? display = null, string? comment = null) : base(parent, identifier, display, comment){
            this.Count = 0;
            this.Amount = amount;
            this.Duration = duration;
        }
    }
    /*#endregion*/


}



