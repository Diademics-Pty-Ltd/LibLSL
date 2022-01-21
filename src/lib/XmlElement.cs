using System;
using System.Runtime.InteropServices;
using LibLSL.Internal;

namespace LibLSL
{
    public sealed class XmlElement 
    {
        private readonly IntPtr _obj;

        public IntPtr Obj => _obj;

        public XmlElement FirstChild => new (DllHandler.lsl_first_child(_obj));
        public XmlElement LastChild => new(DllHandler.lsl_last_child(_obj));
        public XmlElement NextSibling => new(DllHandler.lsl_next_sibling(_obj));
        public XmlElement PreviousSibling => new(DllHandler.lsl_previous_sibling(_obj));
        public XmlElement Parent => new(DllHandler.lsl_parent(_obj));
        public XmlElement Desc => new(DllHandler.lsl_get_desc(_obj));
        public bool Empty => DllHandler.lsl_empty(_obj) != 0;
        public bool IsText => DllHandler.lsl_is_text(_obj) != 0;
        public string Name => Marshal.PtrToStringAnsi(DllHandler.lsl_name(_obj));
        public string Value => Marshal.PtrToStringAnsi(DllHandler.lsl_value(_obj));
        public string ChildValue => Marshal.PtrToStringAnsi(DllHandler.lsl_child_value(_obj));
        public XmlElement AppendChild(string name) => new(DllHandler.lsl_append_child(_obj, name));
        public XmlElement PrependChild(string name) => new(DllHandler.lsl_prepend_child(_obj, name));
        public XmlElement AppendChildValue(string name, string value) => new(DllHandler.lsl_append_child_value(_obj, name, value));
        public XmlElement PrependChildValue(string name, string value) => new(DllHandler.lsl_prepend_child_value(_obj, name, value));
        public XmlElement AppendCopy(XmlElement e) => new(DllHandler.lsl_append_copy(_obj, e.Obj));
        public XmlElement PrependCopy(XmlElement e) => new(DllHandler.lsl_prepend_copy(_obj, e.Obj));
        public void RemoveChild(string name) => DllHandler.lsl_remove_child_n(_obj, name);
        public void RemoveChild(XmlElement e) => DllHandler.lsl_remove_child(_obj, e.Obj);
        public bool SetChildValue(string name, string value) => DllHandler.lsl_set_child_value(_obj, name, value) != 0;
        public bool SetName(string name) => DllHandler.lsl_set_name(_obj, name) != 0;
        public bool SetValue(string value) => DllHandler.lsl_set_value(_obj, value) != 0;
        public string ChildValueFromName(string name) => Marshal.PtrToStringAnsi(DllHandler.lsl_child_value_n(_obj, name));
        public XmlElement Child(string name) => new(DllHandler.lsl_child(_obj, name));
        public XmlElement NextSiblingFromName(string name) => new(DllHandler.lsl_next_sibling_n(_obj, name));
        public XmlElement PreviousSiblingFromName(string name) => new(DllHandler.lsl_previous_sibling_n(_obj, name));
        public XmlElement(IntPtr handle) { _obj = handle; }
    }
}
