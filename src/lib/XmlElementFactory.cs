using LSL.Internal;
using System;

namespace LSL
{
    public static class XmlElementFactory
    {
        public static IXmlElement Create(IntPtr handle) => new XmlElement(handle);
    }
}
