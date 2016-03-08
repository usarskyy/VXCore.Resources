using System;
using System.Reflection;

namespace VXCore.Resources.Exceptions
{
	/// <summary>
	/// The exception that is thrown when an embedded resource can not be found
	/// </summary>
	public class EmbeddedResourceNotFoundException : ApplicationException
	{
		/// <summary>
		/// Assembly to look for an embedded resource
		/// </summary>
		public Assembly SourceAssembly
		{
			get; 
			private set;
		}

		/// <summary>
		/// Full name of the embedded resource
		/// </summary>
		public string ResourceName
		{
			get; 
			private set;
		}

		/// <summary>
		/// Initializes type instance
		/// </summary>
		/// <param name="sourceAssembly">Assebly to look for an embedded resource</param>
		/// <param name="resourceName">Embedded resource name</param>
		public EmbeddedResourceNotFoundException(Assembly sourceAssembly, string resourceName)
			: this(sourceAssembly, resourceName, "Can not find emdedded resource")
		{

		}

		/// <summary>
		/// Initializes type instance
		/// </summary>
		/// <param name="sourceAssembly">Assebly to look for an embedded resource</param>
		/// <param name="resourceName">Embedded resource name</param>
		/// <param name="message">Error message</param>
		public EmbeddedResourceNotFoundException(Assembly sourceAssembly, string resourceName, string message)
			:base(message)
		{
			SourceAssembly = sourceAssembly;
			ResourceName = resourceName;
		}
	}
}