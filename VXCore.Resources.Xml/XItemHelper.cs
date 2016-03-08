using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

using VXCore.Resources.Xml.Exceptions;


namespace VXCore.Resources.Xml
{
    static internal class XItemHelper
    {
        public static ResourceItem ParseNode(ResourceItem parentNode, XElement element)
        {
            Throw.ArgNull(element, "element");

            switch (element.Name.LocalName)
            {
                case "dictionary":
                    return new XDictionaryItem(element);
                case "entry":
                    return new XDictionaryEntryItem(element);
                case "array":
                    return new XArrayItem(parentNode, element);
                case "resource":
                    return new XResourceItem(element);
                case "bool":
                    return new XBoolItem(parentNode, element);
                case "int32":
                    return new XInt32Item(parentNode, element);
                case "string":
                    return new XStringItem(parentNode, element);
                case "switch":
                    return new XSwitchItem(element);
                case "xcase":
                case "case":
                    return ParseCaseNode(parentNode, element);
                case "default":
                    return new XElseItem(parentNode, element);
                default:
                    throw new UnknownNodeNameException(element.Name.LocalName);
            }
        }

        private static XCaseItem ParseCaseNode(ResourceItem parentNode, XElement element)
        {
            XElement firstChild = element.Elements().Single();
            XCaseItem result = new XCaseItem((XSwitchItem) parentNode,
                                             new List<object>(),
                                             ParseNode(parentNode, firstChild));

            if (element.Name.LocalName == "case")
            {
                XAttribute valueAttribute = element.Attribute("value");

                if (valueAttribute == null)
                {
                    throw new ApplicationException("[case] item does not have required [value] attribute");
                }

                string[] values = valueAttribute.Value.Split('|');

                foreach (string value in values)
                {
                    result.Values.Add(value);
                }
            }

            return result;
        }
    }
}
