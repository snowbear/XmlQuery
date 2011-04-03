using System.Collections.Generic;

namespace XmlQuery.Entities
{
    public class Node : Data
    {
        private readonly ICollection<Data> _children = new List<Data>();

        public ICollection<Data> Children { get { return _children; } }
    }
}