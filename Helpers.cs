using ExpaObjects;

namespace Helpers{
   
    public class PPrinter{
        public PPrinter(){
            
        }
    
        public static void PPrint<T>(T[] array){
            Console.WriteLine("[{0}]", String.Join(" , ", array));
            return;
        }
    }
    public static class Extensions{
        public static T[] SubArray<T>(this T[] array, int start, int end){
            T[] result = new T[end - start];
            Array.Copy(array, start,result, 0, end - start);
            return result;
        }
    }

    public static class Converters{
        public static Type StringToType(string input){
            switch(input){
                case "global": return typeof(ExpaGlobal);
                case "nation": return typeof(ExpaNation);
                case "template": return typeof(ExpaTemplate);
                case "time": return typeof(BackgroundObjects.Time);
                default: throw new KeyNotFoundException($"{input} not found in StringToType");
            }
        }
    }
}