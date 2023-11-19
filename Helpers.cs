namespace Helpers
{
    using ExpaObjects;
    using Tokens;
    using Interfaces;
    using BackgroundObjects;
    using Constants;

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
            Console.WriteLine($"***SCOPE***\n\tidentifier = {input.Identifier}\n\ttype={input.TType}\n\t"); 
            PPrint(input.Code);
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
                default: return TokenType.INTERPRETERNULL;
            }
        }
    }
internal static class IsValid{
            public static bool NumberOrMonthChar(char input){
                switch(input){
                    #region numbers
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                    case '0':
                    #endregion
                    case LexerConstants.Chars.DOT:
                    case LexerConstants.Chars.SPACE:
                    case LexerConstants.Chars.COMMA:
                    case '-':
                    case '/':
                        return true;
                    default: return false;

                }
            }
            public static bool ArgumentName(TokenType input){
                switch(input){
                    case TokenType.NATION:
                    case TokenType.SPEED:
                    case TokenType.TIME:
                    case TokenType.DISPLAY:
                    case TokenType.BERTHS:
                    case TokenType.MAXSIZE:
                    case TokenType.MINSIZE:
                    case TokenType.EQUALIZE:
                    case TokenType.MAX:
                    case TokenType.COMMENT:
                        return true;
                    default:
                        return false;
                }
            }
        public static bool IdentifierStartChar(char input) => char.IsLetter(input) || input == LexerConstants.Chars.UNDERSCORE;
        public static bool IdentifierChar(char input) => input != LexerConstants.Chars.SPACE && input != LexerConstants.Chars.TAB && input != LexerConstants.Chars.NEWLINE && (char.IsLetterOrDigit(input) | input == LexerConstants.Chars.UNDERSCORE);
    }
}