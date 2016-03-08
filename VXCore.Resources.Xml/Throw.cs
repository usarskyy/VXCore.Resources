using System;


namespace VXCore.Resources.Xml
{
    internal static class Throw
    {
        public static void ArgNull<T>(T arg, string paramName)
        {
            if (arg == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }

        public static void ArgEmptyOrNull(string arg, string paramName)
        {
            if (string.IsNullOrEmpty(arg))
            {
                throw new ArgumentException("Argument cannot be null or empty", paramName);
            }
        }
    }
}