namespace LibLSL
{
    /// <summary>
    /// Internal exception class for LSL
    /// This was lifted directly from liblsl: https://github.com/labstreaminglayer/liblsl-Csharp/blob/master/LSL.cs#L881-L887
    /// </summary>
    public class InternalException : System.Exception
    {
        public InternalException(string message = "", System.Exception inner = null) { }
        protected InternalException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
        { }
    }
}
