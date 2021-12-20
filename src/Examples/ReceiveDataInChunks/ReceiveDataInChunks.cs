using LSL;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Examples
{
    public sealed class ReceiveDataInChunks : IDisposable
    {
        private readonly Action<float[,], double[]> _onPull;
        private readonly Thread _thread;
        private readonly StreamInlet _streamInlet;
        private readonly float[,] _samples;
        private readonly double[] _timestamps;
        private bool _isRunning = true;
        public ReceiveDataInChunks(Action<float[,], double[]> onPull)
        {
            _onPull = onPull;
            try
            {
                IReadOnlyList<StreamInfo> streamInfos = LSLUtils.ResolveStreams("type", "EEG");
                _streamInlet = new(streamInfos[0]);
                _samples = new float[512, streamInfos[0].Channels];
                _timestamps = new double[512];
            }
            catch (SystemException)
            { }
            _thread = new(PullLoop);
            _thread.Start();
        }
        private void PullLoop()
        {
            double timeNow = LSLUtils.LocalClock;
            while (_isRunning)
            {
                try
                {
                    int sampleCount = _streamInlet.PullChunk(_samples,_timestamps);
                    if (LSLUtils.LocalClock > timeNow + 1)
                    {
                        _onPull(_samples, _timestamps);
                        timeNow = LSLUtils.LocalClock;
                    }
                }
                catch (Exception)
                { }
            }

        }
        public void Dispose()
        {
            _isRunning = false;
            if (_thread.IsAlive)
                _thread.Join();
        }
    }

    public class Program
    {
        private static Action<float[,], double[]> PrintFormattedSamples => (samples, timestamp) =>
            Console.WriteLine($"Received value {samples[0, 0]} on channel 0 at time {timestamp[0]}");

        public static void Main()
        {
            try
            {
                ReceiveDataInChunks receiveDataInChunks = new(PrintFormattedSamples);
            }
            catch (Exception)
            { }
        }
    }
}
