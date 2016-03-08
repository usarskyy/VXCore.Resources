using System;


namespace VXCore.Resources.Exceptions
{
    /// <summary>
    /// The exception that is thrown when resource collection failed to load
    /// </summary>
    public class ResourceLoadException : ApplicationException
    {
        public ResourceLoadException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}