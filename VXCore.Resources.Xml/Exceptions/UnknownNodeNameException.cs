using VXCore.Resources.Exceptions;


namespace VXCore.Resources.Xml.Exceptions
{
	/// <summary>
	/// 
	/// </summary>
    public class UnknownNodeNameException : ResourceLoadException
	{
		public UnknownNodeNameException(string nodeName) :
			base(string.Format("Can not process node with name [{0}]", nodeName), null)
		{
		}
	}
}