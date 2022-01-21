using System;
using LibLSL;
using System.Threading;

namespace SendStringMarkers
{
    class SendStringMarkers
    {
        public static void Main()
        {
            // create stream info and outlet
            using StreamInfo streamInfo = new ("Test1", "Markers", channelFormatType: ChannelFormatType.StringType, sourceIdentifier: "lala");
            using StreamOutlet streamOutlet = new (streamInfo);

            Random rnd = new ();
            string[] strings = new string[] { "Test", "ABC", "123" };
            string[] sample = new string[1];
            for (int k = 0; !Console.KeyAvailable; k++)
            {
                // send a marker and wait for a random interval
                sample[0] = strings[k % 3];
                streamOutlet.PushSample(sample);
                Console.Out.WriteLine(sample[0]);
                Thread.Sleep(rnd.Next(1000));
            }
        }
    }
}
