using System;
using System.Collections.Generic;
using System.Threading;

using VXCore.Resources.Exceptions;


namespace VXCore.Resources
{
    /// <summary>
    /// Base type for resource collections
    /// </summary>
	  public abstract class ResourceCollectionBase
	  {
        private int _loadedMarker = 0;

        /// <summary>
        /// Indiactes if resource was loaded
        /// </summary>
		    public bool IsLoaded
		    {
            get { return _loadedMarker > 0; }
		    }


        /// <summary>
        /// Loads resources
        /// </summary>
        /// <exception cref="ResourceLoadException">Thrown if resource load process has failed</exception>
        public void LoadResources()
        {
            try
            {
                if (Interlocked.Exchange(ref _loadedMarker, 1) == 0)
                {
                    DoLoadResources();
                }
            }
            catch (Exception ex)
            {
                throw new ResourceLoadException("Cound not load resource.", ex);
            }
        }

        /// <summary>
        /// When overridden must implement resource loading logic
        /// </summary>
		    protected abstract void DoLoadResources();

        /// <summary>
        /// When overridden must implement value extraction logic
        /// </summary>
        /// <param name="itemKey">Value (item) key</param>
        /// <returns></returns>
        public object GetItem(string itemKey)
        {
            return GetItem(itemKey, EmptyDictionary<string, string>.Instance);
        }

        /// <summary>
        /// When overridden must implement value extraction logic
        /// </summary>
        /// <param name="itemKey">Value (item) key</param>
        /// <param name="parameters"></param>
        /// <returns></returns>
		    public abstract object GetItem(string itemKey, IDictionary<string, string> parameters);
	  }
}
