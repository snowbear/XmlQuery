using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using NUnit.Framework.Constraints;

namespace XmlQuery.Core.Tests.Extensions
{
    public class CollectionEquivalentResolveConstraint<T> : IResolveConstraint
    {
        private readonly IEnumerable<T> _expected;
        private readonly IEqualityComparer<T> _comparer;

        public CollectionEquivalentResolveConstraint(IEnumerable<T> expected, IEqualityComparer<T> comparer)
        {
            Contract.Requires(expected != null);
            Contract.Requires(comparer != null);
            
            _expected = expected;
            _comparer = comparer;
        }

        public Constraint Resolve()
        {
            return new CollectionEquivalentConstraint<T>(_expected, _comparer);
        }

        [ContractInvariantMethod]
        private void ObjectInvariantMethod()
        {
            Contract.Invariant(_expected != null);
            Contract.Invariant(_comparer != null);
        }
        
        class CollectionEquivalentConstraint<TItem> : CollectionEquivalentConstraint
        {
            private readonly IEnumerable<TItem> _expected;
            private readonly IEqualityComparer<TItem> _comparer;

            public CollectionEquivalentConstraint(IEnumerable<TItem> expected, IEqualityComparer<TItem> comparer)
                : base(expected)
            {
                Contract.Requires(expected != null);
                Contract.Requires(comparer != null);

                _expected = expected;
                _comparer = comparer;
            }

            protected override bool doMatch(IEnumerable actualEnumerable)
            {
                Contract.Assume(actualEnumerable != null);

                return ((IEnumerable<TItem>) actualEnumerable).SequenceEqual(_expected, _comparer);
            }

            [ContractInvariantMethod]
            private void ObjectInvariantMethod()
            {
                Contract.Invariant(_expected != null);
                Contract.Invariant(_comparer != null);
            }
        }
    }
}