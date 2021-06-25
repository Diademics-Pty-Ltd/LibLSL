using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSL.Internal
{
    static class Resolve
    {
        public static IEnumerable<IStreamInfo> ResolveStreams(double waitTime)
        {
            IntPtr[] buf = new IntPtr[1024];
            int num = DllHandler.lsl_resolve_all(buf, (uint)buf.Length, waitTime);
            List<IStreamInfo> streamInfos = new();
            for (int k = 0; k < num; k++)
                streamInfos.Add(StreamInfoFactory.Create(buf[k]));
            return streamInfos;
        }

        public static IEnumerable<IStreamInfo> ResolveStreams(string property, string value, int minimum, double timeout = Constants.Forever)
        {
            IntPtr[] buf = new IntPtr[1024];
            int num = DllHandler.lsl_resolve_byprop(buf, (uint)buf.Length, property, value, minimum, timeout = Constants.Forever);
            List<IStreamInfo> streamInfos = new();
            for (int k = 0; k < num; k++)
                streamInfos.Add(StreamInfoFactory.Create(buf[k]));
            return streamInfos;
        }

        public static IEnumerable<IStreamInfo> ResolveStreams(string predicate, int minimum, double timeout)
        {
            IntPtr[] buf = new IntPtr[1024]; int num = DllHandler.lsl_resolve_bypred(buf, (uint)buf.Length, predicate, minimum, timeout);
            List<IStreamInfo> streamInfos = new();
            for (int k = 0; k < num; k++)
                streamInfos.Add(StreamInfoFactory.Create(buf[k]));
            return streamInfos;
        }
    }
}
