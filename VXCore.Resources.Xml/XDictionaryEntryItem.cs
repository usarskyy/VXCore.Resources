using System.Collections.Generic;
using System.Xml.Linq;


namespace VXCore.Resources.Xml
{
    internal class XDictionaryEntryItem : ResourceItem
    {
        private readonly KeyValuePair<string, string> _value;

        public XDictionaryEntryItem(XElement node)
        {
            Throw.ArgNull(node, "node");

            XAttribute keyAttr = XmlHelper.GetAttrOrThrow(node, XmlHelper.KeyAttr);
            string value = XmlHelper.GetAttrValueOrNull(node, XmlHelper.ValueAttr);

            _value = new KeyValuePair<string, string>(keyAttr.Value, value);
        }

        protected override object DoGetValue(IDictionary<string, string> parameters)
        {
            return _value;
        }
    }
}