using System.Collections.Generic;
using System.Linq;

namespace XmlQuery.Entities
{
    public class DataNode : Data
    {
        private readonly ICollection<Data> _children = new List<Data>();

        public IEnumerable<Data> Children { get { return _children; } }

        public DataNode(string name, IEnumerable<Data> children)
            : base(name)
        {
            _children = children.ToList();
        }
    }
}