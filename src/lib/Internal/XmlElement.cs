using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSL.Internal
{
    internal class XmlElement : IXmlElement
    {
        private readonly IntPtr Obj;

        public XmlElement(IntPtr handle) { Obj = handle; }
    }
}
