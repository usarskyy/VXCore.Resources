using System.Xml.Linq;

using VXCore.Resources.Exceptions;


namespace VXCore.Resources.Xml.Exceptions
{
    public class DuplicatedResourceItemKeyFoundException : ResourceLoadException
    {
        /// <summary>
        /// Gets resource item with found key
        /// </summary>
        public ResourceItem ExistingKeyEntry
        { 
            get; 
            private set;
        }

        /// <summary>
        /// Gets Xml element which has the same key as <see cref="ExistingKeyEntry"/>
        /// </summary>
        public XElement FoundXmlNode
        {
            get; 
            private set;
        }

        public DuplicatedResourceItemKeyFoundException(string keyName, ResourceItem existingKeyEntry, XElement foundXmlNode)
            : base(string.Format("Duplicated resource item key found. Key name is [{0}]", keyName), null)
        {
            ExistingKeyEntry = existingKeyEntry;
            FoundXmlNode = foundXmlNode;
        }
    }
}