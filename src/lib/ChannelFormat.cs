using LibLSL.Properties;

namespace LibLSL
{
    public enum ChannelFormatType
    {
        FloatThirtyTwo = 1,
        DoubleSixtyFour = 2,
        StringType = 3,
        IntThirtyTwo = 4,
        IntSixteen = 5,
        IntEight = 6,
        IntSixtyFour = 7,
        UndefinedType = 0
    }

    public class ChannelFormat
    {
        private const int Float32Size = sizeof(float);
        private const int Double64Size = sizeof(double);
        private const int Int32Size = sizeof(int);
        private const int Int16Size = sizeof(short);
        private const int Int8Size = sizeof(byte);
        private const int Int64Size = sizeof(long);
        private const int StringSize = 0;
        private const int UndefinedSize = -1;
        private readonly ChannelFormatType _type;

        public ChannelFormatType Type => _type;

        internal int Size => _type switch
        {
            ChannelFormatType.FloatThirtyTwo => Float32Size,
            ChannelFormatType.DoubleSixtyFour => Double64Size,
            ChannelFormatType.IntThirtyTwo => Int32Size,
            ChannelFormatType.IntSixteen => Int16Size,
            ChannelFormatType.IntEight => Int8Size,
            ChannelFormatType.IntSixtyFour => Int64Size,
            ChannelFormatType.StringType => StringSize,
            _ => UndefinedSize,
        };

        public override string ToString()
        {
            return _type switch
            {
                ChannelFormatType.FloatThirtyTwo => ChannelFormatStrings.Float32,
                ChannelFormatType.DoubleSixtyFour => ChannelFormatStrings.Double64,
                ChannelFormatType.IntThirtyTwo => ChannelFormatStrings.Int32,
                ChannelFormatType.IntSixteen => ChannelFormatStrings.Int16,
                ChannelFormatType.IntEight => ChannelFormatStrings.Int8,
                ChannelFormatType.IntSixtyFour => ChannelFormatStrings.Int64,
                ChannelFormatType.StringType => ChannelFormatStrings.String,
                _ => ChannelFormatStrings.Undefined,
            };
        }

        public ChannelFormat(ChannelFormatType type) => _type = type;
    }
}
