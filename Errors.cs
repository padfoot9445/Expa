namespace Errors{
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
    public class MainException : System.Exception
    {
        public MainException() { }
        public MainException(string message) : base(message) { }
        public MainException(string message, System.Exception inner) : base(message, inner) { }
        protected MainException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
    public class ExpaWarning{
        static List<ExpaWarning> warnings = new List<ExpaWarning>();
        public ExpaWarning(){
            
        }
    }
}