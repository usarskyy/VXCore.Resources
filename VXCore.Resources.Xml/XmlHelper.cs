using System.Xml.Linq;

using VXCore.Resources.Xml.Exceptions;


namespace VXCore.Resources.Xml
{
    internal static class XmlHelper
    {
        public const string KeyAttr = "key";
        public const string ValueAttr = "value";
        public const string ParameterAttr = "parameter";


        public static string GetAttrValueOrNull(XElement node, string keyAttributeName)
        {
            XAttribute valueAttr = node.Attribute(XName.Get(keyAttributeName));
            string value = valueAttr == null ? node.Value : valueAttr.Value;

            return value;
        }

        public static XAttribute GetAttrOrThrow(XElement node, string keyAttributeName)
        {
            XAttribute keyAttribute = node.Attribute(XName.Get(keyAttributeName));

            if (keyAttribute == null || string.IsNullOrEmpty(keyAttribute.Value))
            {
                throw new AttributeIsMissingException(keyAttributeName, node.Name.LocalName, node);
            }

            return keyAttribute;
        }

        public static void AsserNodeName(XElement node, string resourceNodeName)
        {
            if (!node.Name.LocalName.Equals(resourceNodeName))
            {
                var parentName = node.Parent == null ? string.Empty : node.Parent.Name.LocalName;

                throw new UnexpectedNodeFoundException(node.Name.LocalName, resourceNodeName, parentName);
            }
        }
    }
}