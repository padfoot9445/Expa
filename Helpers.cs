namespace Helpers
{
    using ExpaObjects;
    using Tokens;
    using Interfaces;
    using BackgroundObjects;

    internal static class PPrinter{    
        internal static void PPrint(object? input){
            Console.WriteLine(input);
        }
        internal static void PPrint<T>(T[] array){
            Console.WriteLine("[{0}]", string.Join(" , ", array));
            return;
        }
        internal static void PPrint<T>(List<T> list) => PPrint(list.ToArray());
        internal static void PPrint<T>(HashSet<T> set){
            Console.WriteLine("{{{0}}}", string.Join(" , ", set));
        }
        internal static void PPrint(IHasTime input){
            Console.WriteLine($"Time: {input.Time}");
        }
        internal static void PPrint(ExpaGlobal input){
            PPrint((BaseExpaObject)input);
            PPrint((IHasTime)input);
            Console.WriteLine("PPrint of ExpaGlobal is WIP");
        }
        internal static void PPrint(IHasMinMaxSize input){
            Console.WriteLine($"MinChildShipSize: {input.MinChildShipSize}\n MaxChildShipSize: {input.MaxChildShipSize}");
        }
        internal static void PPrint(ExpaNation input, bool printScope = false){
            PPrint((IHasMinMaxSize)input);
            PPrint((IHasTime)input);
            PPrint((BaseExpaNameSpace)input, printScope);
        }
        internal static void PPrint(ExpaArea input){
            PPrint((IHasMinMaxSize)input);
            PPrint((BaseExpaNameSpace)input);
        }
        internal static void PPrint(BaseExpaObject input){
            Console.WriteLine($"Type: {input.Type}");
            Console.WriteLine($"Display: {input.Display}");
            Console.WriteLine($"Comment: {input.Comment}");
        }
        internal static void PPrint(BaseExpaNameSpace input, bool printScope=false){
            PPrint((BaseExpaNonGlobalObject)input);
            if(printScope){
                PPrint(input.Scope);
            }
            Console.WriteLine("Children:");
            PPrint<string>(input.ChildrenStringIDs);
        }
        internal static void PPrint(BaseExpaNonGlobalObject input){
            Console.WriteLine($"Parent: {input.ParentStringID}");
            Console.WriteLine($"Identifier: {input.StringIdentifier}");
            Console.WriteLine($"ID: {input.StringID}");
        }
        internal static void PPrint(Structs.Scope input){
            Console.WriteLine($"***SCOPE***\n\tidentifier = {input.TokenIdentifier}\n\ttype={input.TType}\n\t"); 
            PPrint<Token>(input.Code);
            Console.WriteLine("***ENDOFSCOPE***");
        }
    }
    internal static class Extensions{
        internal static T[] SubArray<T>(this T[] array, int start, int end){
            T[] result = new T[end - start];
            Array.Copy(array, start,result, 0, end - start);
            return result;
        }
        internal static string Truncate(this string value, int maxLength) => value.Length <= maxLength ? value : value[0..maxLength];
    }

    internal static class Converters{
        internal static TokenType IntToTType(int input){
            switch(input){
                case 1: return TokenType.ONE;
                default: return TokenType.INTERPRETERNULL;
            }
        }
    }
    
}