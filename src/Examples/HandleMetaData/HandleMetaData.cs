using System;
using System.Collections.Generic;
using LSL;

namespace Examples
{
    public sealed class HandleMetaData
    {
        public static void Run()
        {
            // create a new StreamInfo and declare some meta-data (in accordance with XDF format)
            using StreamInfo streamInfo = new("MetaTester", "EEG", 8, 100, ChannelFormatType.FloatThirtyTwo, "myuid323457");
            XmlElement channelElement = streamInfo.Desc.AppendChild("channels");
            string[] channelLabels = { "C3", "C4", "Cz", "FPz", "POz", "CPz", "O1", "O2" };
            for (int k = 0; k < channelLabels.Length; k++)
                _ = channelElement.AppendChild("channel")
                    .AppendChildValue("label", channelLabels[k])
                    .AppendChildValue("unit", "microvolts")
                    .AppendChildValue("type", "EEG");
            _ = streamInfo.Desc.AppendChildValue("manufacturer", "SCCN");
            _ = streamInfo.Desc.AppendChild("cap")
                .AppendChildValue("name", "EasyCap")
                .AppendChildValue("size", "54")
                .AppendChildValue("labelscheme", "10-20");

            // create outlet for the stream
            StreamOutlet streamOutlet = new(streamInfo);

            // === the following could run on another computer ===

            // resolve the stream and open an inlet
            IReadOnlyList<StreamInfo> streamInfos = LSLUtils.ResolveStreams("name", "MetaTester");
            using StreamInlet streamInlet = new(streamInfos[0]);

            // get the full stream info (including custom meta-data) and dissect it
            Console.WriteLine("The stream's XML meta-data is: ");
            Console.WriteLine(streamInlet.StreamInfo.FullMetaDataAsXml);
            Console.WriteLine("The manufacturer is: " + streamInlet.StreamInfo.Desc.ChildValueFromName("manufacturer"));
            Console.WriteLine("The cap circumference is: " + streamInlet.StreamInfo.Desc.Child("cap").ChildValueFromName("size"));
            Console.WriteLine("The channel labels are as follows:");
            channelElement = streamInlet.StreamInfo.Desc.Child("channels").Child("channel");
            for (int k = 0; k < streamInlet.StreamInfo.Channels; k++)
            {
                Console.WriteLine("  " + channelElement.ChildValueFromName("label"));
                channelElement = channelElement.NextSibling;
            }
        }

        public static void Main()
        {
            Run();
            _ = Console.ReadKey();
        }
    }
}
