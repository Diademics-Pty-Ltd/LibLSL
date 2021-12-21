using System;
using LSL;
using System.Threading;

namespace SendDataInChunks
{
    class SendDataInChunks
    {
        public static void Main()
        {
            Random random = new();
            using var streamInfo = new StreamInfo("TestCSharp", "EEG", channels: 8, nominalSamplingRate: 100, sourceIdentifier: "lala");
            using var streamOutlet = new StreamOutlet(streamInfo);
            float[,] data = new float[10, 8];
            Console.WriteLine("Now sending data. Press any key to exit...");
            while (!Console.KeyAvailable)
            {
                // generate random data and send it
                for (int s = 0; s < 10; s++)
                    for (int ch = 0; ch < 8; ch++)
                        data[s, ch] = random.Next(-100, 100);
                streamOutlet.PushChunk(data);
                Thread.Sleep(100);
            }
        }
    }
}
