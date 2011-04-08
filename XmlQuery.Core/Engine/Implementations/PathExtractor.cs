using System;
using System.Collections.Generic;
using System.Linq;
using XmlQuery.Entities;

namespace XmlQuery.Engine.Implementations
{
    public class PathExtractor
    {
        public object ExtractSingleValue(Data root, IEnumerable<string> path)
        {
            var currentNode = root;
            foreach (var pathElement in path)
            {
                if (!(currentNode is DataNode))
                    return DataNotFound.Instance;
                var node = (DataNode) currentNode;
                var selectedNodes = node.Children.Where(c => Equals(c.Name, pathElement));
                if (selectedNodes.Count() > 1)
                    throw new Exception("Data is ambiguous");
                if (!selectedNodes.Any())
                    return DataNotFound.Instance;
                currentNode = selectedNodes.Single();
            }
            if (currentNode is DataAttribute) return ((DataAttribute) currentNode).GetValue();
            return currentNode;
        }
    }
}