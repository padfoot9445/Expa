using BackgroundObjects;
using Markers;
using Structs;
using Tokens;

namespace Interfaces
{
    internal interface IReusable{
        public BaseExpaNonGlobalObject Reuse();
    }
    //*MinSize | MaxSize | Both
    /*#region minSize | maxSize*/
    internal interface IHasMaxSize{
        public int MaxChildShipSize{get; set;}
    }
    internal interface IHasMinSize{
        public int MinChildShipSize{get; set;}
    }
    internal interface IHasMinMaxSize: IHasMaxSize, IHasMinSize{}
    /*#endregion*/
    //*Constructables
    /*#region Constructables*/
    internal interface IConstructable{
        public int Count{get; set;}
        public BackgroundTime Duration{get;}
    }
    internal interface IFastConstructable: IConstructable{
        public int Amount{get;}
    }
    /*#endregion*/
    
    //*Background-Foreground interfaces
    /*#region Background-Foreground interfaces*/
    internal interface IHasTime{
        public BackgroundTime Time{get;}
    }
    internal interface IExpaValue<T> where T : IBackgroundValue {
        public T Value {get;}
    }
    /*#endregion*/
    internal interface IDepender{
        public List<IMDependee> Dependees{get; init;}
    }
    internal interface IBackgroundValue{
        public bool ToBool();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns>Bool | throws ExpaArgumentError</returns>
        public bool Equals(object? other);
        public string ToString();
    }
    internal interface IHasStringID{
        public string StringID{ get; }
    }
    internal interface IExpaNameSpace{ 
        public List<string> ChildrenStringIDs{ get; init; }
        public Scope Scope{ get; init; }
    }
    
}