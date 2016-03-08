using VXCore.Resources.Exceptions;


namespace VXCore.Resources.Xml.Exceptions
{
    public class BoolResourceItemParseFailedException : ResourceLoadException
    {
        public BoolResourceItemParseFailedException(string value)
            : base(string.Format("Cannot parse [boolean] resource item. Original value: [{0}]", value), null)
        {
        }
    }
}