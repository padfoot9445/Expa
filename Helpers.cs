using BackgroundObjects;
using ExpaObjects;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Tokens;
namespace Helpers{
   
    public static class PPrinter{    
        public static void PPrint(object? input){
            Console.WriteLine(input);
        }
        public static void PPrint<T>(T[] array){
            Console.WriteLine("[{0}]", string.Join(" , ", array));
            return;
        }
        public static void PPrint<T>(HashSet<T> set){
            Console.WriteLine("{{{0}}}", string.Join(" , ", set));
        }
        public static void PPrint(IHasTime input){
            Console.WriteLine($"Time: {input.Time}");
        }
        public static void PPrint(ExpaGlobal input){
            PPrint((BaseNameSpace)input);
            PPrint((IHasTime)input);
        }
        public static void PPrint(IHasMinMaxSize input){
            Console.WriteLine($"MinChildShipSize: {input.MinChildShipSize}\n MaxChildShipSize: {input.MaxChildShipSize}");
        }
        public static void PPrint(ExpaNation input, bool printScope = false){
            PPrint((IHasMinMaxSize)input);
            PPrint((IHasTime)input);
            PPrint((BaseNameSpace)input, printScope);
        }
        public static void PPrint(ExpaArea input){
            Console.WriteLine($"MainNationParent: {input.mainNationParent}");
            PPrint((IHasMinMaxSize)input);
            PPrint((BaseNameSpace)input);
        }
        public static void PPrint(BaseObject input){
            Console.WriteLine("identifier:");
            Console.WriteLine(input.TokenIdentifier);
            Console.WriteLine("Parents:");
            PPrint<string>(input.parents);
            Console.WriteLine("Display:");
            Console.WriteLine(input.display);
            Console.WriteLine("Comment:");
            Console.WriteLine(input.comment);
        }
        public static void PPrint(BaseNameSpace input, bool printScope=false){
            if(printScope){
                PPrint(input.scope);
            }
            Console.WriteLine("Children:");
            PPrint<string>(input.children);
            PPrint((BaseObject)input);
        }
        public static void PPrint(Structs.Scope input){
            Console.WriteLine($"***SCOPE***\n\tidentifier = {input.TokenIdentifier}\n\ttype={input.TType}\n\t"); 
            PPrint<Token>(input.Code);
            Console.WriteLine("***ENDOFSCOPE***");
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
                case "time": return typeof(BackgroundObjects.BackgroundTime);
                default: throw new KeyNotFoundException($"{input} not found in StringToType");
            }
        }
        public static TokenType IntToTType(int input){
            switch(input){
                case 1: return TokenType.ONE;
                default: return TokenType.INTERPRETERNULL;
            }
        }
    }
    public static class UID{
        private static readonly HashSet<double> existing = new();
        private static readonly Random rnd = new();
        public static double New(string selfIdentifier, string parentIdentifier){
            //doesn't need to be like this, but it should help prevent collisions
            double tempCode = (selfIdentifier + parentIdentifier).GetHashCode();
            while(existing.Contains(tempCode)){
                tempCode += rnd.Next();
            }
            return tempCode;
        }
    }
    public static class Defaults{
        public const int MINCSS = -1;
        public const int MAXCSS = 10000;
        public static readonly string[] VALIDDBFILEEXTENSIONS = {".sqlite", ". db", ".db3"};
    }
}