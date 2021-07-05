using System.Xml;
using System;

namespace LSL
{
    /// <summary>
    /// Interface representing an LSL StreamInfo object
    /// </summary>
    public interface IStreamInfo : IDisposable
    {
        public string Name { get; }
        public string Type { get; }
        public int Channels { get; }
        public double NominalSamplingRate { get; }
        public ChannelFormatType ChannelFormat { get; }
        public string SourceIdentifier { get; }
        public int Version { get; }
        public double CreatedAt { get; }
        public string UniqueIdentifier { get; }
        public string SessionIdentifier { get; }
        public string SourceHostname { get; }
        public string FullMetaDataAsString { get; }
        public XmlDocument FullMetaDataAsXml { get; }
        public IXmlElement Desc { get; }
        public IntPtr DangerousGetHandle { get; }
    }
}
