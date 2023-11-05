namespace Errors{
    [System.Serializable]
    public abstract class ExpaMainError: System.Exception{
        public ExpaMainError(int line): base($"at line: {line}") { }
        public ExpaMainError(int line, string message) : base($"{message} at line: {line}") { }
        public ExpaMainError(int line, string message, System.Exception inner) : base($"{message} at line: {line}", inner) { }
        protected ExpaMainError(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
    [System.Serializable]
    public class ExpaSyntaxError :ExpaMainError
    {
        public ExpaSyntaxError(int line): base(line) { }
        public ExpaSyntaxError(int line, string message) : base(line, message) { }
        public ExpaSyntaxError(int line, string message, System.Exception inner) : base(line, message, inner) { }
        protected ExpaSyntaxError(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
                }
    [System.Serializable]   
    public class ExpaNameError : ExpaMainError
    {
        public ExpaNameError(int line): base(line) { }
        public ExpaNameError(int line, string message) : base(line, message) { }
        public ExpaNameError(int line, string message, System.Exception inner) : base(line, message, inner) { }
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
    [System.Serializable]
    public class ExpaReassignmentError: ExpaMainError{
        public ExpaReassignmentError(int line): base(line) { }
        public ExpaReassignmentError(int line, string message) : base(line, message) { }
        public ExpaReassignmentError(int line, string message, System.Exception inner) : base(line, message, inner) { }
        protected ExpaReassignmentError(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
        
    }
    [System.Serializable]
    public class ExpaArgumentError : ExpaMainError
    {
        public ExpaArgumentError(int line, string message) : base(line, message){}
        public ExpaArgumentError(int line) : base(line){}
        public ExpaArgumentError(int line, string message, Exception inner) : base(line, message, inner) { }
        protected ExpaArgumentError(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context): base(info, context) { }
    }
    [System.Serializable]
    public class ExpaInterpreterError : ExpaMainError
    {
        public ExpaInterpreterError(int line, string message) : base(line, message){}
        public ExpaInterpreterError(int line) : base(line){}
        public ExpaInterpreterError(int line, string message, Exception inner) : base(line, message, inner) { }
        protected ExpaInterpreterError(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context): base(info, context) { }
    }
    public class ExpaWarning{
        static List<ExpaWarning> warnings = new List<ExpaWarning>();
        public ExpaWarning(){
            
        }
    }
}