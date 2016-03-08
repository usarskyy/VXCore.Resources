using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

using VXCore.Resources.Exceptions;


namespace VXCore.Resources.Xml
{
	internal class XElseItem : ResourceItem
	{
		private readonly ResourceItem _value;

		public XElseItem(ResourceItem parentSwitch, XElement element)
		{
            Throw.ArgNull(parentSwitch, "parentSwitch");
            Throw.ArgNull(element, "element");

		    var children = element.Elements()
		                          .ToList();

            if (children.Count != 1)
            {
                throw new ResourceLoadException(
                    string.Format("'default' switch element expects one single element but found '{0}' elements", children.Count),
                    null);
            }

            XElement childNode = children.SingleOrDefault();
            _value = XItemHelper.ParseNode(parentSwitch, childNode);
		}

		protected override object DoGetValue(IDictionary<string, string> parameters)
		{
            Throw.ArgNull(parameters, "parameters");

			return _value.GetValue(parameters);
		}
	}
}