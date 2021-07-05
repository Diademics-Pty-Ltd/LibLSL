using System;
using System.Runtime.InteropServices;
using System.Xml;

namespace LSL.Internal
{
    internal class StreamInfo : LSLObject, IStreamInfo
    {
        public string Name => Marshal.PtrToStringAnsi(DllHandler.lsl_get_name(Obj));
        public string Type => Marshal.PtrToStringAnsi(DllHandler.lsl_get_type(Obj));
        public int Channels => DllHandler.lsl_get_channel_count(Obj);
        public double NominalSamplingRate => DllHandler.lsl_get_nominal_srate(Obj);
        public ChannelFormatType ChannelFormat => DllHandler.lsl_get_channel_format(Obj);
        public string SourceIdentifier => Marshal.PtrToStringAnsi(DllHandler.lsl_get_source_id(Obj));
        public int Version => DllHandler.lsl_get_version(Obj);
        public double CreatedAt => DllHandler.lsl_get_created_at(Obj);
        public string UniqueIdentifier => Marshal.PtrToStringAnsi(DllHandler.lsl_get_uid(Obj));
        public string SessionIdentifier => Marshal.PtrToStringAnsi(DllHandler.lsl_get_session_id(Obj));
        public string SourceHostname => Marshal.PtrToStringAnsi(DllHandler.lsl_get_hostname(Obj));
        public IXmlElement Desc => XmlElementFactory.Create(DllHandler.lsl_get_desc(Obj));
        public string FullMetaDataAsString
        {
            get
            {
                IntPtr pXml = DllHandler.lsl_get_xml(Obj);
                string strXml = Marshal.PtrToStringAnsi(pXml);
                DllHandler.lsl_destroy_string(pXml);
                return strXml;
            }
        }
        public XmlDocument FullMetaDataAsXml
        { 
            get
            {
                IntPtr pXml = DllHandler.lsl_get_xml(Obj);
                XmlDocument xmlDocument = new();
                xmlDocument.LoadXml(Marshal.PtrToStringAnsi(pXml));
                return xmlDocument;
            }
        }
        public new IntPtr DangerousGetHandle => DangerousGetHandle();

        public StreamInfo(string name,
            string type,
            int channels = 1,
            double nominalSamplingRate = Constants.IrregularRate,
            ChannelFormatType channelFormatType = ChannelFormatType.FloatThirtyTwo,
            string sourceIdentifier = "") : base(DllHandler.lsl_create_streaminfo(name,
            type,
            channels,
            nominalSamplingRate,
            channelFormatType,
            sourceIdentifier))
        { }

        public StreamInfo(IntPtr handle) : base(handle) { }

        protected override void DestroyLSLObject(IntPtr obj) { _ = DllHandler.lsl_destroy_streaminfo(obj); }
    }
}
