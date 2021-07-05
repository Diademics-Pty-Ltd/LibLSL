using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSL
{
    /// <summary>
    /// Interface representing the StreamOutlet object.
    /// </summary>
    public interface IStreamOutlet : IDisposable
    {
        /// <summary cref="WaitForConsumers">
        /// Set this action to fire when WaitForConsumersAsync is completed.
        /// </summary>
        public Action<bool> OnGotConsumer { get; set; }

        /// <summary>
        /// Ask if this outlet is feeding any inlets.
        /// </summary>
        public bool HaveConsumers { get; }

        /// <summary>
        /// Get the full stream information about this outlet.
        /// </summary>
        public IStreamInfo Info { get; }

        /// <summary>
        /// Wait until an inlet connects before consuming resources.
        /// </summary>
        /// <param name="timeout"></param>
        /// <returns>True when a consumer appears, false if timeout is reached.</returns>
        public bool WaitForConsumers(double timeout);

        /// <summary cref="WaitForConsumers(double)">
        /// Async wrapper for WaitForConsumers
        /// </summary>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public async Task WaitForConsumersAsync(double timeout)
        {
            bool result = await Task.Run(() => WaitForConsumers(timeout));
            OnGotConsumer?.Invoke(result);
        }

        /// <summary>
        /// Group of templated methods for pushing a single sample.
        /// </summary>
        /// <param name="data">Datum</param>
        /// <param name="timestamp">Optional timestamp (note, this may be overriden by configuration files)</param>
        /// <param name="pushthrough">Whether to push the sample through to the receivers instead of buffering 
        /// it with subsequent samples.Note that the chunk_size, if specified at outlet construction,
        /// takes precedence over the pushthrough flag.</param>
        public void PushSample(float[] data, double timestamp = 0.0, bool pushthrough = true);
        public void PushSample(double[] data, double timestamp = 0.0, bool pushthrough = true);
        public void PushSample(int[] data, double timestamp = 0.0, bool pushthrough = true);
        public void PushSample(short[] data, double timestamp = 0.0, bool pushthrough = true);
        public void PushSample(char[] data, double timestamp = 0.0, bool pushthrough = true);
        public void PushSample(string[] data, double timestamp = 0.0, bool pushthrough = true);

        /// <summary>
        /// Group of templated methods for pushing a chunk of data. 
        /// </summary>
        /// <param name="data">Data is organized mxn where
        /// m is the sample index and n is the channel number.</param>
        /// <param name="timestamp">Optional first timestamp of the chunk. An array of timestamps corresponding to 
        /// each sample in the chunk may also be supplied.</param>
        /// <param name="pushthrough">Whether to push the chunk through to the receivers instead of buffering in
        /// with subsequent samples.Note that the chunk_size, if specified at outlet construction, takes
        /// precedence over the pushthrough flag.</param>
        public void PushChunk(float[,] data, double timestamp = 0.0, bool pushthrough = true);
        public void PushChunk(double[,] data, double timestamp = 0.0, bool pushthrough = true);
        public void PushChunk(int[,] data, double timestamp = 0.0, bool pushthrough = true);
        public void PushChunk(short[,] data, double timestamp = 0.0, bool pushthrough = true);
        public void PushChunk(char[,] data, double timestamp = 0.0, bool pushthrough = true);
        public void PushChunk(string[,] data, double timestamp = 0.0, bool pushthrough = true);

        public void PushChunk(float[,] data, double[] timestamps, bool pushthrough = true);
        public void PushChunk(double[,] data, double[] timestamps, bool pushthrough = true);
        public void PushChunk(int[,] data, double[] timestamps, bool pushthrough = true);
        public void PushChunk(short[,] data, double[] timestamps, bool pushthrough = true);
        public void PushChunk(char[,] data, double[] timestamps, bool pushthrough = true);
        public void PushChunk(string[,] data, double[] timestamps, bool pushthrough = true);
    }
}
