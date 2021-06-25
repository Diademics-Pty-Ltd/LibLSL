using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSL.Internal
{
    internal class XmlElement : IXmlElement
    {
        private readonly IntPtr _obj;

        public IntPtr Obj =>_obj;

        public IXmlElement FirstChild => XmlElementFactory.Create(DllHandler.lsl_first_child(_obj));
        public IXmlElement LastChild => XmlElementFactory.Create(DllHandler.lsl_last_child(_obj));
        public IXmlElement NextSibling => XmlElementFactory.Create(DllHandler.lsl_next_sibling(_obj));
        public IXmlElement PreviousSibling => XmlElementFactory.Create(DllHandler.lsl_previous_sibling(_obj));
        public IXmlElement Parent => XmlElementFactory.Create(DllHandler.lsl_parent(_obj));
        public bool Empty => (DllHandler.lsl_empty(_obj) != 0);
        public bool IsText => (DllHandler.lsl_is_text(_obj) != 0);
        public string Name => Marshal.PtrToStringAnsi(DllHandler.lsl_name(_obj));
        public string Value => Marshal.PtrToStringAnsi(DllHandler.lsl_value(_obj));
        public string ChildValue => Marshal.PtrToStringAnsi(DllHandler.lsl_child_value(_obj));

        public IXmlElement AppendChild(string name) => XmlElementFactory.Create(DllHandler.lsl_append_child(_obj, name));
        public IXmlElement PrependChild(string name) => XmlElementFactory.Create(DllHandler.lsl_prepend_child(_obj, name));
        public IXmlElement AppendChildValue(string name, string value) => XmlElementFactory.Create(DllHandler.lsl_append_child_value(_obj, name, value));
        public IXmlElement PrependChildValue(string name, string value) => XmlElementFactory.Create(DllHandler.lsl_prepend_child_value(_obj, name, value));
        public IXmlElement AppendCopy(IXmlElement e) => XmlElementFactory.Create(DllHandler.lsl_append_copy(_obj, e.Obj));
        public IXmlElement PrependCopy(IXmlElement e) => XmlElementFactory.Create(DllHandler.lsl_prepend_copy(_obj, e.Obj));
        public void RemoveChild(string name) => DllHandler.lsl_remove_child_n(_obj, name);
        public void RemoveChild(IXmlElement e) => DllHandler.lsl_remove_child(_obj, e.Obj);
        public bool SetChildValue(string name, string value) => (DllHandler.lsl_set_child_value(_obj, name, value)!=0);
        public bool SetName(string name) => (DllHandler.lsl_set_name(_obj, name) != 0);
        public bool SetValue (string value) => (DllHandler.lsl_set_value(_obj, value) != 0);
        public string ChildValueFromName(string name) => Marshal.PtrToStringAnsi(DllHandler.lsl_child_value_n(_obj, name));
        public IXmlElement Child(string name) => XmlElementFactory.Create(DllHandler.lsl_child(_obj, name));
        public IXmlElement NextSiblingFromName(string name) => XmlElementFactory.Create(DllHandler.lsl_next_sibling_n(_obj, name));
        public IXmlElement PreviousSiblingFromName(string name) => XmlElementFactory.Create(DllHandler.lsl_previous_sibling_n(_obj, name));
        
        public XmlElement(IntPtr handle) { _obj = handle; }
    }
}
