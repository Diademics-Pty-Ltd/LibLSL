using System;
using LSL.Properties;

namespace LSL.Internal
{
    internal static class Error
    { 
        public static void CheckError(int ec)
        {
            if (ec < 0)
                throw ec switch
                {
                    -1 => new TimeoutException(ExceptionMessages.Timeout),
                    -2 => new LostException(ExceptionMessages.StreamLost),
                    -3 => new ArgumentException(ExceptionMessages.Argument),
                    -4 => new InternalException(ExceptionMessages.Internal),
                    _ => new Exception(ExceptionMessages.Anything),
                };
        }
    }
}
