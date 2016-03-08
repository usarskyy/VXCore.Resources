using System.Collections.Generic;
using System.Xml.Linq;

using VXCore.Resources.Xml.Exceptions;


namespace VXCore.Resources.Xml
{
    internal class XSwitchItem : ResourceItem
    {
        public string ParameterName
        {
            get;
            private set;
        }

        public IList<XCaseItem> Cases
        {
            get;
            private set;
        }

        public XElseItem DefaultItem
        {
            get;
            private set;
        }

        public XSwitchItem(XElement element)
        {
            Throw.ArgNull(element, "element");

            XAttribute paramName = XmlHelper.GetAttrOrThrow(element, XmlHelper.ParameterAttr);
            Cases = new List<XCaseItem>();
            ParameterName = paramName.Value;

            foreach (XElement childNode in element.Elements())
            {
                switch (childNode.Name.LocalName)
                {
                    case "case":
                    case "xcase":
                        Cases.Add((XCaseItem) XItemHelper.ParseNode(this, childNode));
                        break;
                    case "default":
                        DefaultItem = (XElseItem) XItemHelper.ParseNode(this, childNode);
                        break;
                    default:
                        throw new UnknownNodeNameException(childNode.Name.LocalName);
                }
            }
        }

        protected override object DoGetValue(IDictionary<string, string> parameters)
        {
            object result = null;

            if (parameters != null)
            {
                if (!parameters.ContainsKey(ParameterName))
                {
                    if (DefaultItem != null)
                    {
                        return DefaultItem.GetValue(parameters);
                    }

                    return null;
                }

                foreach (XCaseItem caseItem in Cases)
                {
                    string parameter = parameters[ParameterName];
                    if (caseItem.Values.Contains(parameter))
                    {
                        result = caseItem.GetValue(parameters);
                        break;
                    }
                }
            }

            return result ?? (DefaultItem != null ? DefaultItem.GetValue(parameters) : null);
        }
    }
}