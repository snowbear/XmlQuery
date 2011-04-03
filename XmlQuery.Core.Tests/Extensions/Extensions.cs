namespace XmlQuery.Core.Tests.Extensions
{
    public static class Extensions
    {
        public static T Cast<T>(this object o)
            where T : class
        {
            return o as T;
        }
    }
}