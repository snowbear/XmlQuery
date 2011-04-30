using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace XmlQuery.Entities
{
    public class DataNode : Data
    {
        [ContractPublicPropertyName("Children")]
        private readonly ICollection<Data> _children = new List<Data>();

        public IEnumerable<Data> Children { get { return _children; } }

        public DataNode(string name, IEnumerable<Data> children)
            : base(name)
        {
            Contract.Requires(name != null);
            Contract.Requires(children != null);

            _children = children.ToList();
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(_children != null);
        }
    }
}