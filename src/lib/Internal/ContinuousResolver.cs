using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LSL.Internal
{
    internal class ContinuousResolver : LSLObject, IContinuousResolver
    {

        public Action<IEnumerable<IStreamInfo>> OnGotResult { get; set; }

        public ContinuousResolver(double forgetAfter = 5.0)
            : base(DllHandler.lsl_create_continuous_resolver(forgetAfter)) { }

        public ContinuousResolver(string property, string value, double forgetAfter = 5.0)
            : base(DllHandler.lsl_create_continuous_resolver_byprop(property, value, forgetAfter)) { }

        public ContinuousResolver(string predicate, double forgetAfter = 5.0)
            : base(DllHandler.lsl_create_continuous_resolver_bypred(predicate, forgetAfter)) { }

        public IEnumerable<IStreamInfo> Results()
        {
            IntPtr[] buf = new IntPtr[1024];
            int streams = DllHandler.lsl_resolver_results(Obj, buf, (uint)buf.Length);
            List<IStreamInfo> streamInfos = new();
            for (int k = 0; k < streams; k++)
                streamInfos.Add(StreamInfoFactory.Create((buf[k])));
            return streamInfos;
        }

        protected override void DestroyLSLObject(IntPtr obj)
        {
            DllHandler.lsl_destroy_continuous_resolver(obj);
        }

    }
}
