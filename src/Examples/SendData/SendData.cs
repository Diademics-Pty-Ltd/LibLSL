﻿using System;
using LibLSL;
using System.Threading;

namespace Examples
{
    public class SendData
    {
        public static void Main()
        {
            Random random = new();
            using var streamInfo = new StreamInfo("TestCSharp", "EEG", channels: 8, nominalSamplingRate: 100, sourceIdentifier: "lala");
            using var streamOutlet = new StreamOutlet(streamInfo);
            float[] data = new float[8];
            Console.WriteLine("Now sending data. Press any key to exit...");
            while (!Console.KeyAvailable)
            {
                // generate random data and send it
                for (int ch = 0; ch < data.Length; ch++)
                    data[ch] = random.Next(-100, 100);
                streamOutlet.PushSample(data);
                Thread.Sleep(10);
            }
        }
    }
}
