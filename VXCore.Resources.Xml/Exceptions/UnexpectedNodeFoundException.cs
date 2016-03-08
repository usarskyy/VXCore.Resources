using VXCore.Resources.Exceptions;


namespace VXCore.Resources.Xml.Exceptions
{
    public class UnexpectedNodeFoundException : ResourceLoadException
    {
        public UnexpectedNodeFoundException(string foundNodeName, string expectedNodeName)
            : base(string.Format("Unexpected node [{0}] found. Expected node is [{1}]", foundNodeName, expectedNodeName), null)
        {
        }

        public UnexpectedNodeFoundException(string foundNodeName, string expectedNodeName, string parentNodeName)
            : base(string.Format("Unexpected node [{0}] found, parent node is [{1}]. Expected node is [{2}]", foundNodeName, parentNodeName, expectedNodeName), null)
        {
        }
    }
}
