using System;
using System.IO;
using System.Reflection;
using System.Text;

using VXCore.Resources.Exceptions;


namespace VXCore.Resources.Extensions
{
	/// <summary>
	/// Contains set of methods that simplify access to the assembly's embedded resources
	/// </summary>
	public static class AssemblyExtensions
	{
		/// <summary>
		/// Reads embedded resource
		/// </summary>
		/// <param name="resourceAssembly">Assebly to look for an embedded resource</param>
		/// <param name="resourceName">Embedded resource full name</param>
		/// <returns>Embedded resource content</returns>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="resourceAssembly"/> is null</exception>
		/// <exception cref="ArgumentException">Thrown when <paramref name="resourceName"/> is null or empty string</exception>
		public static string ReadEmbeddedResource(this Assembly resourceAssembly, string resourceName)
		{
			return ReadEmbeddedResource(resourceAssembly, resourceName, false);
		}

		/// <summary>
		/// Reads embedded resource
		/// </summary>
		/// <param name="resourceAssembly">Assebly to look for an embedded resource</param>
		/// <param name="resourceName">Embedded resource full name</param>
		/// <param name="throwException">Indiactes if an exception has to be thrown when an embedded resource cannot be found</param>
		/// <returns>Embedded resource content</returns>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="resourceAssembly"/> is null</exception>
		/// <exception cref="ArgumentException">Thrown when <paramref name="resourceName"/> is null or empty string</exception>
		/// <exception cref="EmbeddedResourceNotFoundException">Thrown when embedded resource was not found and <paramref name="throwException"/> is set to <c>true</c></exception>
		public static string ReadEmbeddedResource(this Assembly resourceAssembly, string resourceName, bool throwException)
		{
			if (resourceAssembly == null)
			{
				throw new ArgumentNullException("resourceAssembly");
			}

			if (string.IsNullOrEmpty(resourceName))
			{
				throw new ArgumentException("Resource name cannot be null or empty", "resourceName");
			}

			using (Stream stream = resourceAssembly.GetManifestResourceStream(resourceName))
			{
				if (stream == null)
				{
					throw new EmbeddedResourceNotFoundException(resourceAssembly, resourceName);
				}

				stream.Position = 0;

				using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
				{
					return reader.ReadToEnd();
				}
			}
		}
	}
}