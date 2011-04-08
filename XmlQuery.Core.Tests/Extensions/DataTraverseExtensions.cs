using System.Linq;
using XmlQuery.Entities;

namespace XmlQuery.Core.Tests.Extensions
{
    public static class DataTraverseExtensions
    {
        public static Data FirstChild(this Data data)
        {
            var node = (DataNode)data;
            return node.Children.First();
        }

        public static Data NthChild(this Data data, int index)
        {
            var node = (DataNode)data;
            return node.Children.ElementAt(index);
        }
    }
}