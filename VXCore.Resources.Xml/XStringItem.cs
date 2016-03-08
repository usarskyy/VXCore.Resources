using System.Collections.Generic;
using System.Xml.Linq;


namespace VXCore.Resources.Xml
{
    internal class XStringItem : ResourceItem
    {
        private readonly string _value;

        public XStringItem(ResourceItem parentItem, XElement node)
        {
            Throw.ArgNull(parentItem, "parentItem");
            Throw.ArgNull(node, "node");

            ParentItem = parentItem;
            _value = node.Value;
        }

        public ResourceItem ParentItem { get; private set; }

        protected override object DoGetValue(IDictionary<string, string> parameters)
        {
            return _value;
        }
    }
}