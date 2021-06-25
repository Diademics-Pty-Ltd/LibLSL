using System;

namespace LSL
{
    public interface IXmlElement
    {
        public IntPtr Obj { get; }

        public IXmlElement FirstChild { get; }
        public IXmlElement LastChild { get; }
        public IXmlElement NextSibling { get; }
        public IXmlElement PreviousSibling { get; }
        public IXmlElement Parent { get; }
        public bool Empty { get; }
        public bool IsText { get; }
        public string Name { get; }
        public string Value { get; }
        public string ChildValue { get; }

        public IXmlElement AppendChild(string name);
        public IXmlElement PrependChild(string name);
        public IXmlElement AppendChildValue(string name, string value);
        public IXmlElement PrependChildValue(string name, string value);
        public IXmlElement AppendCopy(IXmlElement e);
        public IXmlElement PrependCopy(IXmlElement e);
        public void RemoveChild(string name);
        public void RemoveChild(IXmlElement e);
        public bool SetChildValue(string name, string value);
        public bool SetName(string name);
        public bool SetValue(string value);
        public string ChildValueFromName(string name);
        public IXmlElement Child(string name);
        public IXmlElement NextSiblingFromName(string name);
        public IXmlElement PreviousSiblingFromName(string name);
    }
}
