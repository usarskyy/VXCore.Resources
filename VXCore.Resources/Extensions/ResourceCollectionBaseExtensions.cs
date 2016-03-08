using System;
using System.Collections.Generic;


namespace VXCore.Resources.Extensions
{
    public static class ResourceCollectionBaseExtensions
    {
        public static T GetItem<T>(this ResourceCollectionBase collection, string itemKey, IDictionary<string, string> parameters)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }

            return (T) collection.GetItem(itemKey, parameters);
        }
    }
}