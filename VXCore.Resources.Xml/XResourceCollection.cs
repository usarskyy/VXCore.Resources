using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

using VXCore.Resources.Extensions;
using VXCore.Resources.Xml.Exceptions;


namespace VXCore.Resources.Xml
{
    public class XResourceCollection : ResourceCollectionBase
    {
        private readonly XDocument _xdoc;
        private readonly IDictionary _items = new HybridDictionary();
      
        private ValidationEventHandler _validationEventHandler;
        private ValidationEventArgs _validatedArgs;

        public XResourceCollection(XDocument xdoc, bool loadResources = true)
        {
            Throw.ArgNull(xdoc, "xdoc");

            _xdoc = xdoc;
            _validationEventHandler = (sender, args) =>
            {
                _validatedArgs = args;
            };
            
            if (loadResources)
            {
                LoadResources();

                _xdoc = null;
            }
        }

        protected override void DoLoadResources()
        {
            using(var contentReader = new StringReader(GetType().ReadEmbeddedResource("VXCore.Resources.Xml.ValidationSchema.xsd", true)))
            {
                using (var xmlReader = XmlReader.Create(contentReader))
                {
                    XmlSchemaSet schemas = new XmlSchemaSet();
                    schemas.Add("urn:vxcore:resources", xmlReader);
                    
                    _xdoc.Validate(schemas, _validationEventHandler, true);
                }
            }

            if (_validatedArgs != null)
            {
                _validationEventHandler = null;

                throw new XsdValidationException(_validatedArgs.Message, _validatedArgs.Exception);
            }

            LoadXmlFile(_xdoc);
        }

        public override object GetItem(string itemKey, IDictionary<string, string> parameters)
        {
            Throw.ArgEmptyOrNull(itemKey, "itemKey");
            
            ResourceItem ri = (ResourceItem) _items[itemKey];

            return ri == null ? null : ri.GetValue(parameters ?? EmptyDictionary<string, string>.Instance);
        }

        public string[] GetResourceNames()
        {
            return _items.Keys.Cast<string>().ToArray();
        }

        private void LoadXmlFile(XDocument document)
        {
            Throw.ArgNull(document, "document");

            XElement rootNode = document.Root;

            if (rootNode == null)
            {
                return;
            }

            const string rootNodeName = "resources";

            if (!rootNode.Name.LocalName.Equals(rootNodeName))
            {
                throw new UnexpectedNodeFoundException(rootNode.Name.LocalName, rootNodeName);
            }

            foreach (XElement node in rootNode.Elements())
            {
                XmlHelper.AsserNodeName(node, "resource");

                LoadResourceNode(node);
            }
        }

        private void LoadResourceNode(XElement node)
        {
            Throw.ArgNull(node, "node");

            XAttribute keyAttribute = XmlHelper.GetAttrOrThrow(node, XmlHelper.KeyAttr);

            if (_items.Contains(keyAttribute.Value))
            {
                throw new DuplicatedResourceItemKeyFoundException(keyAttribute.Value,
                                                                  (ResourceItem) _items[keyAttribute.Value],
                                                                  node);
            }

            ResourceItem ri = XItemHelper.ParseNode(null, node);

            _items.Add(keyAttribute.Value, ri);
        }
    }
}
