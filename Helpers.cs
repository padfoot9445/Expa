namespace helpers{
    [System.Serializable]
    public class ExpaSyntaxError : System.Exception
    {
        public ExpaSyntaxError(int line): base($"at line: {line}") { }
        public ExpaSyntaxError(int line, string message) : base($"{message} at line: {line}") { }
        public ExpaSyntaxError(int line, string message, System.Exception inner) : base($"{message} at line: {line}", inner) { }
        protected ExpaSyntaxError(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
                }
    [System.Serializable]   
    public class ExpaNameError : System.Exception
    {
        public ExpaNameError(int line): base($"at line: {line}") { }
        public ExpaNameError(int line, string message) : base($"{message} at line: {line}") { }
        public ExpaNameError(int line, string message, System.Exception inner) : base($"{message} at line: {line}", inner) { }
        protected ExpaNameError(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
    [System.Serializable]
    public class mainException : System.Exception
    {
        public mainException() { }
        public mainException(string message) : base(message) { }
        public mainException(string message, System.Exception inner) : base(message, inner) { }
        protected mainException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
    public class ExpaWarning{
        static List<ExpaWarning> warnings = new List<ExpaWarning>();
        public ExpaWarning(){
            
        }
    }
    public class PPrinter{
        public PPrinter(){
            
        }
    
        public static void PPrint<T>(T[] array){
            Console.WriteLine("[{0}]", String.Join(',', array));
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
}