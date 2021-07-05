using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LSL
{
    /// <summary>
    /// Interface for accessing the LSL convenience methods for stream resolution. Once
    /// this object is created, call Results() for a list of StreamInfo objects associated with
    /// each available LSL stream currently active.
    /// </summary>
    public interface IContinuousResolver : IDisposable
    {
        /// <summary>
        /// Set this action to fire after a background call to Results().
        /// </summary>
        public Action<IEnumerable<IStreamInfo>> OnGotResult { get; set; }

        /// <summary>
        /// Get a list of StreamInfo objects associated to every available stream on the network.
        /// </summary>
        /// <returns cref="IStreamInfo">The return type is any collection implementing IEnumerable of objects implementing the
        /// IStreamInfo Object.</returns>
        public IEnumerable<IStreamInfo> Results();

        /// <summary cref="Results()>
        /// Async wrapper for Results()
        /// </summary>
        /// <returns></returns>
        public async Task ResultsAsync()
        {
            IEnumerable<IStreamInfo> streamInfos = await Task.Run(() => Results());
            OnGotResult?.Invoke(streamInfos);
        }
    }
}
