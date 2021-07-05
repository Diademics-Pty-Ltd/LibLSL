using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LSL
{
    public interface IContinuousResolver : IDisposable
    {
        public Action<IEnumerable<IStreamInfo>> OnGotResult { get; set; }
        public IEnumerable<IStreamInfo> Results();
        public async Task ResultsAsync()
        {
            IEnumerable<IStreamInfo> streamInfos = await Task.Run(() => Results());
            OnGotResult?.Invoke(streamInfos);
        }
    }
}
