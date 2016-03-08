using System.Collections.Generic;
using System.Xml.Linq;

using VXCore.Resources.Xml.Exceptions;


namespace VXCore.Resources.Xml
{
    /// <summary>
    /// Represents boolean value.
    /// Xml form: &lt;bool&gt;false&lt;/bool&gt;
    /// </summary>
    internal class XBoolItem : ResourceItem
    {
        private readonly bool _value;

        public ResourceItem ParentItem
        {
            get;
            private set;
        }

        public XBoolItem(ResourceItem parentItem, XElement node)
        {
            Throw.ArgNull(parentItem, "parentItem");
            Throw.ArgNull(node, "node");

            ParentItem = parentItem;

            if (!bool.TryParse(node.Value, out _value))
            {
                throw new BoolResourceItemParseFailedException(node.Value);
            }
        }

        protected override object DoGetValue(IDictionary<string, string> parameters)
        {
            return _value;
        }
    }
}