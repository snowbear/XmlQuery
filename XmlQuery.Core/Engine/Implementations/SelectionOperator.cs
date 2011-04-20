using System.Collections.Generic;
using System.Linq;
using XmlQuery.Entities;

namespace XmlQuery.Engine.Implementations
{
    public class SelectionOperator
    {
        private readonly IPredicate _predicate;

        public SelectionOperator(IPredicate predicate)
        {
            _predicate = predicate;
        }

        public IEnumerable<Data> ProcessData(IEnumerable<Data> input)
        {
            return input.Where(_predicate.Check);
        }
    }
}