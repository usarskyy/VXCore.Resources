using VXCore.Resources.Exceptions;


namespace VXCore.Resources.Xml.Exceptions
{
    public class Int32ResourceItemParseFailedException : ResourceLoadException
    {
        public Int32ResourceItemParseFailedException(string value)
            : base(string.Format("Cannot parse [int32] resource item. Original value: [{0}]",
            value), null)
        {
        }
    }
}