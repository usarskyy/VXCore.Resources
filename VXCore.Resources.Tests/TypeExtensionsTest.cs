using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using VXCore.Resources.Extensions;


namespace VXCore.Resources.Tests
{
	[TestFixture]
	public class TypeExtensionsTest
	{
		[Test]
		public void TestReadEmbeddedResource_WithLatinCharset()
		{
			string text = typeof (TypeExtensionsTest).ReadEmbeddedResource("VXCore.Resources.Tests.Latin.txt");

			Assert.IsTrue(string.Equals("wertDFGfg 567 $#%^&", text));
		}

		[Test]
		public void TestReadEmbeddedResource_WithCyrilicCharset()
		{
			string text = typeof(TypeExtensionsTest).ReadEmbeddedResource("VXCore.Resources.Tests.Cyrilic.txt");

			Assert.IsTrue(string.Equals("WS івапр вапрвсмиты 85 ;%:)(*", text));
		}
	}
}
