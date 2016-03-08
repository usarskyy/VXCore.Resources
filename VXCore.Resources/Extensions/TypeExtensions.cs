using System;

using VXCore.Resources.Exceptions;


namespace VXCore.Resources.Extensions
{
	/// <summary>
	/// Contains set of methods that simplify access to the embedded resources located
	/// in the same assembly like specific type
	/// </summary>
	public static class TypeExtensions
	{
		/// <summary>
		/// Reads embedded resource that is located in the same asembly like a type
		/// </summary>
		/// <param name="type">Type that will be used as an reference to the assembly to look for an embedded resource for</param>
		/// <param name="resourceName">Embedded resource full name</param>
		/// <returns>Embedded resource content</returns>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="type"/> is null</exception>
		/// <exception cref="ArgumentException">Thrown when <paramref name="resourceName"/> is null or empty string</exception>
		public static string ReadEmbeddedResource(this Type type, string resourceName)
		{
			return ReadEmbeddedResource(type, resourceName, false);
		}

		/// <summary>
		/// Reads embedded resource that is located in the same asembly like a type
		/// </summary>
		/// <param name="type">Type that will be used as an reference to the assembly to look for an embedded resource for</param>
		/// <param name="resourceName">Embedded resource full name</param>
		/// <param name="throwException">Indiactes if an exception has to be thrown when an embedded resource cannot be found</param>
		/// <returns>Embedded resource content</returns>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="type"/> is null</exception>
		/// <exception cref="ArgumentException">Thrown when <paramref name="resourceName"/> is null or empty string</exception>
		/// <exception cref="EmbeddedResourceNotFoundException">Thrown when embedded resource was not found and <paramref name="throwException"/> is set to <c>true</c></exception>
		public static string ReadEmbeddedResource(this Type type, string resourceName, bool throwException)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}

			if (string.IsNullOrEmpty(resourceName))
			{
				throw new ArgumentException("Resource name cannot be null or empty", "resourceName");
			}

			return type.Assembly.ReadEmbeddedResource(resourceName, throwException);
		}
	}
}