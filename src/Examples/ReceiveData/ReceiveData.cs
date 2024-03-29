﻿using LibLSL;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Examples
{
    public sealed class ReceiveData : IDisposable
    {
        private readonly Action<float[], double> _onPull;
        private readonly Thread _thread;
        private readonly StreamInlet _streamInlet;
        private readonly float[] _sample;
        private bool _isRunning = true;
        public ReceiveData(Action<float[], double> onPull)
        {
            _onPull = onPull;
            try
            {
                IReadOnlyList<StreamInfo> streamInfos = LSLUtils.ResolveStreams("type", "EEG");


                _streamInlet = new(streamInfos[0]);
                _sample = new float[streamInfos[0].Channels];
            }
            catch (InternalException)
            {
                // handle internal LSL error here
            }

            _thread = new(PullLoop);
            _thread.Start();
        }
        private void PullLoop()
        {
            double timeNow = LSLUtils.LocalClock;
            double timestamp = 0.0;
            while (_isRunning)
            {
                try
                {
                    timestamp = _streamInlet.PullSample(_sample);
                }
                catch (InternalException)
                {
                    // handle internal LSL error here
                }
                if (LSLUtils.LocalClock > timeNow + 1)
                {
                    _onPull(_sample, timestamp);
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

    public class Program
    {
        private static Action<float[], double> PrintFormattedSamples => (samples, timestamp) =>
            Console.WriteLine($"Received value {samples[0]} on channel 0 at time {timestamp}");

        public static void Main()
        {
            try
            {
                ReceiveData receiveData = new(PrintFormattedSamples);
            }
            catch (Exception)
            { }
        }
    }
}
