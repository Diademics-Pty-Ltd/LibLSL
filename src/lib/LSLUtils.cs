using System;
using System.Collections.Generic;

namespace LSL
{
    public static class LSLUtils
    {
        public static int ProtocolVersion => DllHandler.lsl_protocol_version();
        public static int LibraryVersion => DllHandler.lsl_library_version();
        public static double LocalClock => DllHandler.lsl_local_clock();

        public static IReadOnlyList<StreamInfo> ResolveStreams(double waitTime = 5.0)
        {
            IntPtr[] buf = new IntPtr[1024];
            int num = DllHandler.lsl_resolve_all(buf, (uint)buf.Length, waitTime);
            List<StreamInfo> streamInfos = new();
            for (int k = 0; k < num; k++)
                streamInfos.Add(new(buf[k]));
            return streamInfos;
        }

        public static IReadOnlyList<StreamInfo> ResolveStreams(string property, string value, int minimum = 1, double waitTime = 5.0)
        {
            IntPtr[] buf = new IntPtr[1024];
            int num = DllHandler.lsl_resolve_byprop(buf, (uint)buf.Length, property, value, minimum, waitTime);
            List<StreamInfo> streamInfos = new();
            for (int k = 0; k < num; k++)
                streamInfos.Add(new(buf[k]));
            return streamInfos;
        }

        public static IEnumerable<StreamInfo> ResolveStreams(string predicate, int minimum, double waitTime = 5.0)
        {
            IntPtr[] buf = new IntPtr[1024]; int num = DllHandler.lsl_resolve_bypred(buf, (uint)buf.Length, predicate, minimum, waitTime);
            List<StreamInfo> streamInfos = new();
            for (int k = 0; k < num; k++)
                streamInfos.Add(new(buf[k]));
            return streamInfos;
        }
    }
}
