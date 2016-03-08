using System.Collections.Generic;
using System.Collections.Specialized;
using System.Xml.Linq;


namespace VXCore.Resources.Xml
{
    internal class XDictionaryItem : ResourceItem
    {
        private readonly List<ResourceItem> _items = new List<ResourceItem>();

        public XDictionaryItem(XElement node)
        {
            Throw.ArgNull(node, "node");

            IEnumerable<XElement> elements = node.Elements();

            foreach (XElement element in elements)
            {
                ResourceItem entry = XItemHelper.ParseNode(this, element);

                _items.Add(entry);
            }
        }

        protected override object DoGetValue(IDictionary<string, string> parameters)
        {
            Throw.ArgNull(parameters, "parameters");

            NameValueCollection result = new NameValueCollection();
            
            // todo: it can be cached, dictionary item cannot contain other elements than "string->string" pair
            
            foreach (ResourceItem item in _items)
            {
                KeyValuePair<string, string> pair = (KeyValuePair<string, string>) item.GetValue(parameters);

                result.Add(pair.Key, pair.Value);
            }

            return result;
        }
    }
}