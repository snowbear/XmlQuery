using System;
using System.Diagnostics.Contracts;
using System.Linq;
using XmlQuery.Entities;

namespace XmlQuery.Core.Tests.Extensions
{
    public static class DataTraverseExtensions
    {
        public static Data FirstChild(this Data data)
        {
            Contract.Requires(data != null);
            Contract.Requires(((DataNode) data).Children != null);

            return NthChild(data, 0);
        }

        public static Data NthChild(this Data data, int index)
        {
            Contract.Requires(data != null);
            Contract.Requires(((DataNode)data).Children != null);
            Contract.Requires(index >= 0);

            var node = (DataNode)data;
            return node.Children.ElementAt(index);
        }
    }
}