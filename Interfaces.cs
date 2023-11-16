using BackgroundObjects;
using Metadata;
using Structs;

namespace Interfaces
{
    public interface IReusable{
        public void Reuse();
    }
    //*MinSize | MaxSize | Both
    /*#region minSize | maxSize*/
    public interface IHasMaxSize{
        public int MaxChildShipSize{get; set;}
    }
    public interface IHasMinSize{
        public int MinChildShipSize{get; set;}
    }
    public interface IHasMinMaxSize: IHasMaxSize, IHasMinSize{}
    /*#endregion*/
    //*Constructables
    /*#region Constructables*/
    public interface IConstructable{
        public int Count{get; set;}
        public BackgroundTime Duration{get;}
    }
    public interface IFastConstructable: IConstructable{
        public int Amount{get;}
    }
    /*#endregion*/
    
    //*Background-Foreground interfaces
    /*#region Background-Foreground interfaces*/
    public interface IHasTime{
        public BackgroundTime Time{get;}
    }
    public interface IExpaValue<T> where T : IBackgroundValue {
        public T Value {get;}
    }
    /*#endregion*/
    public interface IDepender{
        public List<IDependee> Dependees{get; set;}
    }
    public interface IBackgroundValue{
        public bool ToBool();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns>Bool | throws ExpaArgumentError</returns>
        public bool Equals(object? other);
        public string ToString();
    }
    public interface IExpaNonGlobalObject: IExpaObject{
        public string ParentStringID{ get; init; }
        public string Display{ get; set; }
        public string Comment{ get; set; }
        public string StringIdentifier{ get; }
        public string StringID();

    }
    public interface INameSpace: IExpaNonGlobalObject{
        public List<string> ChildrenStringIDs{ get; }
        public Scope Scope{ get; }
    }
    
}