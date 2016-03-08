using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

using VXCore.Resources.Exceptions;


namespace VXCore.Resources.Xml
{
	/// <summary>
	/// 
	/// </summary>
	internal class XResourceItem : ResourceItem
	{
		private readonly ResourceItem _childItem;

        public string Name { get; private set; }
        
		public XResourceItem(XElement node)
		{
            Throw.ArgNull(node, "node");

            var keyAttribute = XmlHelper.GetAttrOrThrow(node, XmlHelper.KeyAttr);
		    var resourceNodeChildren = node.Elements().ToList();

		    if (resourceNodeChildren.Count != 1)
		    {
		        throw new ResourceLoadException(
                    string.Format("Resource element '{0}' expects one single element but found '{1}' elements", keyAttribute.Value, resourceNodeChildren.Count),
		            null);
		    }

		    Name = keyAttribute.Value;
            _childItem = XItemHelper.ParseNode(this, resourceNodeChildren.SingleOrDefault());
		}

		/// <summary>
		/// Gets the value.
		/// </summary>
		/// <param name="parameters">The parameters.</param>
		/// <returns></returns>
		protected override object DoGetValue(IDictionary<string, string> parameters)
		{
            Throw.ArgNull(parameters, "parameters");

		    try
		    {
		        return _childItem.GetValue(parameters);
		    }
		    catch (Exception ex)
		    {
		        throw new ResourceLoadException(string.Format("Failed to get value from '{0}' resource element", Name), ex);
		    }
		}
	}
}