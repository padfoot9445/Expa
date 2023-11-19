namespace Helpers
{
    using ExpaObjects;
    using Tokens;
    using Interfaces;
    using BackgroundObjects;

    public static class PPrinter{    
        public static void PPrint(object? input){
            Console.WriteLine(input);
        }
        public static void PPrint<T>(T[] array){
            Console.WriteLine("[{0}]", string.Join(" , ", array));
            return;
        }
        public static void PPrint<T>(List<T> list) => PPrint(list.ToArray());
        public static void PPrint<T>(HashSet<T> set){
            Console.WriteLine("{{{0}}}", string.Join(" , ", set));
        }
        public static void PPrint(IHasTime input){
            Console.WriteLine($"Time: {input.Time}");
        }
        public static void PPrint(ExpaGlobal input){
            PPrint((BaseExpaObject)input);
            PPrint((IHasTime)input);
            Console.WriteLine("PPrint of ExpaGlobal is WIP");
        }
        public static void PPrint(IHasMinMaxSize input){
            Console.WriteLine($"MinChildShipSize: {input.MinChildShipSize}\n MaxChildShipSize: {input.MaxChildShipSize}");
        }
        public static void PPrint(ExpaNation input, bool printScope = false){
            PPrint((IHasMinMaxSize)input);
            PPrint((IHasTime)input);
            PPrint((BaseExpaNameSpace)input, printScope);
        }
        public static void PPrint(ExpaArea input){
            PPrint((IHasMinMaxSize)input);
            PPrint((BaseExpaNameSpace)input);
        }
        public static void PPrint(BaseExpaObject input){
            Console.WriteLine($"Type: {input.Type}");
            Console.WriteLine($"Display: {input.Display}");
            Console.WriteLine($"Comment: {input.Comment}");
        }
        public static void PPrint(BaseExpaNameSpace input, bool printScope=false){
            PPrint((BaseExpaNonGlobalObject)input);
            if(printScope){
                PPrint(input.Scope);
            }
            Console.WriteLine("Children:");
            PPrint<string>(input.ChildrenStringIDs);
        }
        public static void PPrint(BaseExpaNonGlobalObject input){
            Console.WriteLine($"Parent: {input.ParentStringID}");
            Console.WriteLine($"Identifier: {input.StringIdentifier}");
            Console.WriteLine($"ID: {input.StringID}");
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
        public static string Truncate(this string value, int maxLength) => value.Length <= maxLength ? value : value[0..maxLength];
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
}