using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSL.Internal
{
    internal class ContinuousResolver : LSLObject
    {

        public Action<IEnumerable<IStreamInfo>> OnGotResult { get;  set; } = null; 

        public ContinuousResolver(double forgetAfter = 5.0)
            : base(DllHandler.lsl_create_continuous_resolver(forgetAfter)){ }

        public ContinuousResolver(string prop, string value, double forgetAfter = 5.0)
            : base(DllHandler.lsl_create_continuous_resolver_byprop(prop, value, forgetAfter)) { }

        public ContinuousResolver(string pred, double forgetAfter = 5.0)
            : base(DllHandler.lsl_create_continuous_resolver_bypred(pred, forgetAfter)) { }

        public IEnumerable<IStreamInfo> Results()
        {
            IntPtr[] buf = new IntPtr[1024];
            int streams = DllHandler.lsl_resolver_results(Obj, buf, (uint)buf.Length);
            List<IStreamInfo> streamInfos = new();
            for (int k = 0; k < streams; k++)
                streamInfos.Add(StreamInfoFactory.Create((buf[k])));
            return streamInfos;
        }

        public async Task ResultsAsync()
        {
            IEnumerable<IStreamInfo> streamInfos = await Task.Run(() => Results());
            OnGotResult?.Invoke(streamInfos);
        }

        protected override void DestroyLSLObject(IntPtr obj)
        {
            DllHandler.lsl_destroy_continuous_resolver(obj);
        }

    }
}
