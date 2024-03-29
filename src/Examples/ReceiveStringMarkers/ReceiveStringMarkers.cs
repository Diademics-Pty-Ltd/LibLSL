﻿using LibLSL;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Examples
{
    public sealed class ReceiveStringMarkers : IDisposable
    {
        private readonly Action<string[], double> _onPull;
        private readonly Thread _thread;
        private readonly StreamInlet _streamInlet;
        private readonly string[] _markers;
        private bool _isRunning = true;
        public ReceiveStringMarkers(Action<string[], double> onPull)
        {
            _onPull = onPull;
            try
            {
                IReadOnlyList<StreamInfo> streamInfos = LSLUtils.ResolveStreams("type", "Markers");
                if(streamInfos.Count==0)
                {
                    Console.WriteLine("No stream of type Markers available. Exiting program.");
                    return;
                }    
                _streamInlet = new(streamInfos[0]);
                _markers = new string[streamInfos[0].Channels];
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
                double timestamp = 0.0;
                try
                {
                    timestamp = _streamInlet.PullSample(_markers);
                }
                catch (InternalException)
                {
                    // handle internal LSL error here
                }
                _onPull(_markers, timestamp);
                timeNow = LSLUtils.LocalClock;

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
        private static Action<string[], double> PrintFormattedMarkers => (markers, timestamp) =>
            Console.WriteLine($"Received value {markers[0]} on channel 0 at time {timestamp}");

        public static void Main()
        {
            try
            {
                ReceiveStringMarkers receiveStringMarkers = new(PrintFormattedMarkers);
            }
            catch (Exception)
            { }
        }
    }
}
