using System.Collections.Generic;
using System.Linq;

namespace XmlQuery.Entities
{
    public class Node : Data
    {
        private readonly ICollection<Data> _children = new List<Data>();

        public ICollection<Data> Children { get { return _children; } }

        public Node(string name, IEnumerable<Data> children)
        {
            Name = name;
            _children = children.ToList();
        }
    }
}