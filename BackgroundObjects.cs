using Structs;

namespace BackgroundObjects
{
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
    
}