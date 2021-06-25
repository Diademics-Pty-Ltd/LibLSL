using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LSL.Internal;

namespace LSL
{
    public static class StreamInfoFactory
    {
        public static IStreamInfo Create(string name,
            string type,
            int channels,
            double nominalSamplingRate,
            ChannelFormatType channelFormatType,
            string sourceIdentifier = "") => new StreamInfo(name,
            type,
            channels,
            nominalSamplingRate,
            channelFormatType,
            sourceIdentifier);

        public static IStreamInfo Create(IntPtr handle) => new StreamInfo(handle);
    }
}
