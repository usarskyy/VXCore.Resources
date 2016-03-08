using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;


namespace VXCore.Resources.Xml
{
    internal class XArrayItem : ResourceItem
    {
        private readonly ResourceItem[] _arrayItems;

        public XArrayItem(ResourceItem parentItem, XElement node)
        {
            Throw.ArgNull(parentItem, "parentItem");
            Throw.ArgNull(node, "node");

            IEnumerable<XElement> elements = node.Elements();

            _arrayItems = elements.Select(element => XItemHelper.ParseNode(parentItem, element))
                                  .ToArray();
        }

        protected override object DoGetValue(IDictionary<string, string> parameters)
        {
            Throw.ArgNull(parameters, "parameters");

            ArrayList result = new ArrayList();

            foreach (ResourceItem arrayItem in _arrayItems)
            {
                result.Add(arrayItem.GetValue(parameters));
            }

            return result;
        }
    }
}