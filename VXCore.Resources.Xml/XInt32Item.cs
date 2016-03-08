using System.Collections.Generic;
using System.Xml.Linq;

using VXCore.Resources.Xml.Exceptions;


namespace VXCore.Resources.Xml
{
    public class XInt32Item : ResourceItem
    {
        private readonly int _value;

        public ResourceItem ParentItem
        {
            get;
            private set;
        }

        public XInt32Item(ResourceItem parentItem, XElement node)
        {
            Throw.ArgNull(parentItem, "parentItem");
            Throw.ArgNull(node, "node");

            ParentItem = parentItem;

            if (!int.TryParse(node.Value, out _value))
            {
                throw new Int32ResourceItemParseFailedException(node.Value);
            }
        }

        protected override object DoGetValue(IDictionary<string, string> parameters)
        {
            return _value;
        }
    }
}