using System.Collections.Generic;


namespace VXCore.Resources
{
    /// <summary>
    /// Base type for elements that are used to create <see cref="ResourceCollectionBase"/>
    /// </summary>
	public abstract class ResourceItem
	{
        /// <summary>
        /// Returns item's value by provided <paramref name="parameters"/>
        /// </summary>
        /// <param name="parameters">Parameters used to resolve item's value</param>
        /// <returns></returns>
        public object GetValue(IDictionary<string, string> parameters)
        {
            return DoGetValue(parameters ?? EmptyDictionary<string, string>.Instance);
        }

        /// <summary>
        /// When overriden returns item's value by provided <paramref name="parameters"/>
        /// </summary>
        /// <param name="parameters">Parameters used to resolve item's value</param>
        /// <returns></returns>
        protected abstract object DoGetValue(IDictionary<string, string> parameters);
	}
}
