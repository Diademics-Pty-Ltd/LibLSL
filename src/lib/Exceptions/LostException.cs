namespace LSL
{
    /// <summary>
    /// Lost exception class for LSL
    /// This was lifted directly from liblsl: https://github.com/labstreaminglayer/liblsl-Csharp/blob/master/LSL.cs#L870-L876
    /// </summary>
    internal class LostException : System.Exception
    {
        public LostException(string message = "", System.Exception inner = null) { }
        protected LostException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
        { }
    }
}
