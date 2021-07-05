using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSL
{
    public interface IStreamInlet : IDisposable
    {
        /// <summary cref="PullSampleAsync">
        /// Set this action to fire when PullSampleAsync is completed.
        /// </summary>
        public Action<double> OnGotNewSample { get; set; }

        public Action<int> OnGotNewChunk { get; set; }

        public int SamplesAvailable { get; }

        public bool WasClockReset { get; }

        public IStreamInfo Info(double timeout = Constants.Forever);

        public void OpenStream(double timeout = Constants.Forever);

        public void CloseStream();

        public double TimeCorrection(double timeout = Constants.Forever);

        public PostProcessingOptions PostProcessingOptions { get; set; }

        public double PullSample(float[] sample, double timeout = Constants.Forever);
        public double PullSample(double[] sample, double timeout = Constants.Forever);
        public double PullSample(int[] sample, double timeout = Constants.Forever);
        public double PullSample(short[] sample, double timeout = Constants.Forever);
        public double PullSample(char[] sample, double timeout = Constants.Forever);
        public double PullSample(string[] sample, double timeout = Constants.Forever);

        public async Task PullSampleAsync(float[] sample, double timeout = Constants.Forever)
        {
            double timestamp = await Task.Run(() => PullSample(sample, timeout));
            OnGotNewSample?.Invoke(timestamp);
        }

        public int PullChunk(float[,] data, double[] timestamps, double timeout = 0.0);
        public int PullChunk(double[,] data, double[] timestamps, double timeout = 0.0);
        public int PullChunk(int[,] data, double[] timestamps, double timeout = 0.0);
        public int PullChunk(short[,] data, double[] timestamps, double timeout = 0.0);
        public int PullChunk(char[,] data, double[] timestamps, double timeout = 0.0);
        public int PullChunk(string[,] data, double[] timestamps, double timeout = 0.0);

        public async Task PullChunkAsync(float[,] data, double[] timestamps, double timeout = 0.0)
        {
            int chunkSize = await Task.Run(() => PullChunk(data, timestamps, timeout));
            OnGotNewSample?.Invoke(chunkSize);
        }

    }
}
