namespace ExpaObjects
{
    using Tokens;
    using Structs;
    using BackgroundObjects;
    using Helpers;
    using Markers;
    using Interfaces;
    using Defaults;
    using Parser;
    using Errors;
    using ChildrenIDList = List<string>;
    using Constants;

    #region Namespaces
    internal class ExpaGlobal : BaseExpaObject, IExpaNameSpace, IHasTime, IMCanBeParent<ExpaNation>{
        public BackgroundTime Time{ get; private set; }
        public override TokenType Type => TokenType.GLOBAL;
        public override bool IsNameSpace => true;

        public ChildrenIDList ChildrenStringIDs { get; init; }

        public Scope Scope { get; init; }

        internal ExpaGlobal(
            Scope scope,
            string display = ExpaObjectConstants.GLOBAL_DEFAULT_DISPLAY, 
            string? comment = null
        ): base(
            ExpaObjectConstants.GLOBAL_IDENTIFIER,
            display,
            comment
        ){
            this.Time = new(0, 0);
            this.Scope = scope;
            this.ChildrenStringIDs = new();
        }
        internal ExpaGlobal(
            Scope scope, 
            BackgroundTime time, 
            string display = ExpaObjectConstants.GLOBAL_DEFAULT_DISPLAY,
            string? comment = null
        ): base(
            ExpaObjectConstants.GLOBAL_IDENTIFIER,
            display,
            comment
        ){
            this.Time = time;
            this.ChildrenStringIDs = new();
            this.Scope = scope;
        }
    }
    internal class ExpaNation: BaseExpaNameSpace, IExpaNameSpace, IHasTime, IMCanBeParent<ExpaNation>, IMCanBeParent<ExpaArea>, IMCanBeParent<ExpaFunction>, IMCanBeParent<ExpaTemplate>, IHasMinMaxSize{
        public BackgroundTime Time{get; private set;}
        public int MaxChildShipSize{get; set;}
        public int MinChildShipSize{get; set;}
        public override TokenType Type => TokenType.NATION;
        #region Constructor
        public ExpaNation(
            string parentStringID,
            string identifier,
            Scope scope,
            BackgroundTime time, 
            ChildrenIDList? ChildrenStringIDs = null,
            int MinChildShipSize = Defaults.MINCSS, 
            int MaxChildShipSize = Defaults.MAXCSS,
            string? display = null, 
            string? comment = null
        ):base(
            parentStringID,//validate at caller
            identifier,
            scope,
            ChildrenStringIDs is null? new(): ChildrenStringIDs,
            display,
            comment
        ){//handle implicit time on caller side; Global gets its time by modify and not set.
            this.Time = time;
            this.MinChildShipSize = MinChildShipSize;
            this.MaxChildShipSize = MaxChildShipSize;
        }
        #endregion
    }
    internal class ExpaArea : BaseExpaNameSpace, IExpaNameSpace, IMCanBeParent<ExpaArea>,IMCanBeParent<ExpaFunction>, IHasMinMaxSize{
        public int MinChildShipSize{get; set;}
        public int MaxChildShipSize{get; set;}
        public override TokenType Type => TokenType.AREA;
        #region Constructor
        public ExpaArea(
            string parentStringID,
            string identifier,
            Scope aScope, 
            ChildrenIDList? childrenStringIDs = null,
            int minCSS = Defaults.MINCSS,
            int maxCSS = Defaults.MAXCSS,
            string? display = null, 
            string? comment = null
        ):base(
            parentStringID, 
            identifier,
            aScope, 
            childrenStringIDs is not null ? childrenStringIDs: new(),
            display, 
            comment
        ){
            this.MinChildShipSize = minCSS;
            this.MaxChildShipSize = maxCSS;
        }
        #endregion
    }
    internal class ExpaFunction: BaseExpaNameSpace, IExpaNameSpace, IReusable, IMCanBeParent<ExpaFunction>{
        public StaticCommand[] Commands{ get; set; }
        public override TokenType Type => TokenType.FUNCTION;
        #region constructor
        public ExpaFunction(
            string parentStringID, 
            string identifier,
            Scope scope,
            ChildrenIDList? childrenIDList = null,
            string? display = null, 
            string? comment = null
        ):base(
            parentStringID, 
            identifier,
            scope, 
            childrenIDList is not null? childrenIDList: new(),
            display, 
            comment
        ){
            Commands = Array.Empty<StaticCommand>();
        }
        #endregion
        public BaseExpaNonGlobalObject Reuse(){
            throw new NotImplementedException();
        }
        
    }
    internal class ExpaTemplate: BaseExpaNameSpace, IReusable{
        public bool Equalize{ get; set; }
        public override TokenType Type => TokenType.TEMPLATE;
        #region Constructor
        public ExpaTemplate(
            string parentStringID, 
            string identifier,
            Scope scope,
            bool equalize, 
            ChildrenIDList? childrenIDList = null,
            string? display = null, 
            string? comment = null
        ):base(
            parentStringID, 
            identifier,
            scope, 
            childrenIDList is not null? childrenIDList: new(),
            display, 
            comment
        ){
            this.Equalize = equalize;
        }
        #endregion
        public BaseExpaNonGlobalObject Reuse(){
            throw new NotImplementedException();
        }
    }
    #endregion
    
    #region Values
        #region NonPrimitiveValues
    internal class ExpaTime: BaseExpaNonGlobalObject, IExpaValue<BackgroundTime>{//not IHasTime because the time object itself does not have a time, and also it doesn't make sense programtically.
        public BackgroundTime Value{get; private set;}

        public override TokenType Type => TokenType.TIME;

        public ExpaTime(
            string parentID, 
            string identifier, 
            BackgroundTime time, 
            string? display = null, 
            string? comment = null
        ):base(
            parentID, 
            identifier, 
            display, 
            comment
        ){
            Value = time;
        }
    }
        #endregion
        
        #region Primitives
    internal class ExpaNumber : BaseExpaNonGlobalObject, IExpaValue<BackgroundNumber>{
        public BackgroundNumber Value{ get; private set; }

        public override TokenType Type => TokenType.NUMBER;

        public ExpaNumber(string parentStringID, string identifier, BackgroundNumber value, string? display = null, string? comment = null) : base(parentStringID, identifier, display, comment) => this.Value = value;
        #region operators
        public static double operator +(ExpaNumber self, object? other) => (double)(self.Value + other);
        public static double operator -(ExpaNumber self, object? other) => (double)(self.Value - other);
        public static double operator *(ExpaNumber self, object? other) => (double)(self.Value * other);
        public static double operator /(ExpaNumber self, object? other) => (double)(self.Value / other);
        #endregion
        #region comparisons
        public static bool operator ==(ExpaNumber self, object? other) => self.Value == other;
        public static bool operator !=(ExpaNumber self, object? other) => !(self.Value == other);
        public override bool Equals(object? other) => this == other;
        #endregion
        public override int GetHashCode() => Value.GetHashCode();
    }
    internal class ExpaBool : BaseExpaNonGlobalObject, IExpaValue<BackgroundBool>{
        public BackgroundBool Value{ get; private set; }

        public override TokenType Type => TokenType.BOOL;

        public ExpaBool(
            string parentStringID, 
            string identifier, 
            BackgroundBool value, 
            string? display = null, 
            string? comment = null
        ):base(
            parentStringID, 
            identifier, 
            display, 
            comment
        ) => this.Value = value;       
    }
    internal class ExpaString : BaseExpaNonGlobalObject, IExpaValue<BackgroundString>{
        public BackgroundString Value{ get; private set; }
        public override TokenType Type => TokenType.STRING;

        public ExpaString(
            string parentStringID, 
            string identifier, 
            BackgroundString value, 
            string? display = null, 
            string? comment = null
        ):base(
            parentStringID, 
            identifier, 
            display, 
            comment
        ) => this.Value = value;

        public static string operator +(ExpaString self, object? other) => (string)(self.Value + other);
    }
        #endregion
    #endregion
    #region Constructables
    internal class ExpaShipClass : BaseExpaNonGlobalObject, IConstructable{
        public int Count{get; set;}
        public BackgroundTime Duration{get; private set;}

        public override TokenType Type => TokenType.SHIPCLASS;

        public ExpaShipClass(
            string parentStringID, 
            string identifier, 
            BackgroundTime duration, 
            string? display = null, 
            string? comment = null
        ):base(
            parentStringID,
            identifier,
            display,
            comment
        ){
            this.Duration = duration;
            this.Count = 0;
        }
    }
    internal class ExpaComponent : BaseExpaNonGlobalObject, IFastConstructable{
        public int Amount{get; private set;}

        public int Count {get; set;}

        public BackgroundTime Duration{get; private set;}

        public override TokenType Type => TokenType.COMPONENT;

        public ExpaComponent(
            string parentStringID,
            string identifier,
            BackgroundTime duration,
            int amount,
            string? display = null,
            string? comment = null
        ):base(
            parentStringID,
            identifier,
            display,
            comment
        ){
            this.Count = 0;
            this.Amount = amount;
            this.Duration = duration;
        }
    }
    #endregion
}



