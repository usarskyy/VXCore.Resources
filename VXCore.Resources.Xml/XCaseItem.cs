using System.Collections.Generic;


namespace VXCore.Resources.Xml
{
    internal class XCaseItem : ResourceItem
    {
        private readonly XSwitchItem _parentSwitchItem;

        public IList<object> Values
        {
            get;
            private set;
        }

        private readonly ResourceItem _result;

        public XCaseItem(XSwitchItem parentSwitchItem, IList<object> values, ResourceItem result)
        {
            Throw.ArgNull(parentSwitchItem, "parentSwitchItem");
            Throw.ArgNull(values, "values");
            Throw.ArgNull(result, "result");

            _parentSwitchItem = parentSwitchItem;
            Values = values;
            _result = result;
        }

        protected override object DoGetValue(IDictionary<string, string> parameters)
        {
            Throw.ArgNull(parameters, "parameters");

            if (parameters[_parentSwitchItem.ParameterName] == null)
            {
                return null;
            }

            string parameterValue = parameters[_parentSwitchItem.ParameterName];

            if (!Values.Contains(parameterValue))
            {
                return null;
            }

            return _result.GetValue(parameters);
        }
    }
}