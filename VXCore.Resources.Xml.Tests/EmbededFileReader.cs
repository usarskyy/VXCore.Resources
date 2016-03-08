using System.IO;


namespace VXCore.Resources.Xml.Tests
{
	internal class EmbededFileReader
	{
		private readonly static EmbededFileReader _instance = new EmbededFileReader();

		public static EmbededFileReader Instance
		{
			get
			{
				return _instance;
			}
		}


		public string Read(string name)
		{
            using (Stream stream = GetType().Assembly.GetManifestResourceStream(name))
			{
				using (StreamReader sr = new StreamReader(stream))
				{
					return sr.ReadToEnd();
				}
			}
		}
	}
}
