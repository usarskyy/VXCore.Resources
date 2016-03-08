using System;


namespace VXCore.Resources.Xml.Exceptions
{
    public class XsdValidationException : Exception
    {
        public XsdValidationException(string message, Exception ex) : base(message, ex) { }
    }
}