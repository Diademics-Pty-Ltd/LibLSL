using LibLSL.Properties;

namespace LibLSL.Internal
{
    internal static class Error
    {
        public static void Check(int ec)
        {
            if (ec < 0)
                throw ec switch
                {
                    -1 => new InternalException(ExceptionMessages.Timeout),
                    -2 => new InternalException(ExceptionMessages.StreamLost),
                    -3 => new InternalException(ExceptionMessages.Argument),
                    -4 => new InternalException(ExceptionMessages.Internal),
                    _ => new InternalException(ExceptionMessages.Anything),
                };
        }
    }
}
