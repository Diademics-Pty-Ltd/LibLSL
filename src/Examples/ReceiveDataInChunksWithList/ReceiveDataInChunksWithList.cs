using LibLSL;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Examples
{
    public sealed class ReceiveDataInChunksWithList : IDisposable
    {
        private readonly Action<List<List<float>>, List<double>> _onPull;
        private readonly Thread _thread;
        private readonly StreamInlet _streamInlet;
        private readonly List<List<float>> _samples;
        private readonly List<double> _timestamps;
        private bool _isRunning = true;
        public ReceiveDataInChunksWithList(Action<List<List<float>>, List<double>> onPull)
        {
            _onPull = onPull;
            try
            {
                IReadOnlyList<StreamInfo> streamInfos = LSLUtils.ResolveStreams("type", "EEG");
                _streamInlet = new(streamInfos[0], maxChunkLength: 5);
                _samples = new();
                _timestamps = new();
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
                    _streamInlet.PullChunk(_samples, _timestamps);
                }
                catch (InternalException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                if ((LSLUtils.LocalClock > timeNow + 1) && (_samples.Count > 0))
                {
                    _onPull(_samples, _timestamps);
                    timeNow = LSLUtils.LocalClock;
                }
            }
        }

        public void Dispose()
        {
            _isRunning = false;
            if (_thread.IsAlive)
                _thread.Join();
        }
    }
    internal class Program
    {
        private static Action<List<List<float>>, List<double>> PrintFormattedSamples => (samples, timestamp) =>
            Console.WriteLine($"Received {samples.Count} values, first: {samples[0][0]} on channel 0 at time {timestamp[0]}");

        public static void Main()
        {
            try
            {
                ReceiveDataInChunksWithList receiveDataInChunks = new(PrintFormattedSamples);
            }
            catch (Exception)
            { }
        }
    }
}
