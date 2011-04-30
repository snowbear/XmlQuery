using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using XmlQuery.Entities;

namespace XmlQuery.Engine.Implementations
{
    public class SelectionOperator
    {
        private readonly IPredicate _predicate;

        public SelectionOperator(IPredicate predicate)
        {
            Contract.Requires(predicate != null);

            _predicate = predicate;
        }

        public IEnumerable<Data> ProcessData(IEnumerable<Data> input)
        {
            Contract.Requires(input != null);

            return input.Where(_predicate.Check);
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(_predicate != null);
        }
    }
}