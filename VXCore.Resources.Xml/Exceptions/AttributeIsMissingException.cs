using System.Xml.Linq;

using VXCore.Resources.Exceptions;


namespace VXCore.Resources.Xml.Exceptions
{
    public class AttributeIsMissingException : ResourceLoadException
    {
        public XElement Element { get; private set; }

        public AttributeIsMissingException(string attributeName, string parentNodeName, XElement element)
            : base(string.Format("Required attribute [{0}] can not be found. Parent node name is [{1}]", attributeName, parentNodeName), null)
        {
            Element = element;
        }
    }
}
