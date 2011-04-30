using System;
using System.Collections;
using System.Diagnostics.Contracts;
using System.Globalization;
using XmlQuery.Entities;

namespace XmlQuery.Engine.Implementations
{
    public class ComparePredicate : IPredicate
    {
        private readonly IDataSelector _firstArgumentSelector;
        private readonly IDataSelector _secondArgumentSelector;
        private readonly Func<int, bool> _checkComparisonResult;

        public ComparePredicate(IDataSelector firstArgumentSelector, IDataSelector secondArgumentSelector, CompareType type)
        {
            Contract.Requires(firstArgumentSelector != null);
            Contract.Requires(secondArgumentSelector != null);

            _firstArgumentSelector = firstArgumentSelector;
            _secondArgumentSelector = secondArgumentSelector;

            switch (type)
            {
                case CompareType.Less:
                    _checkComparisonResult = r => r < 0;
                    break;
                case CompareType.LessOrEqual:
                    _checkComparisonResult = r => r <= 0;
                    break;
                case CompareType.Greater:
                    _checkComparisonResult = r => r > 0;
                    break;
                case CompareType.GreaterOrEqual:
                    _checkComparisonResult = r => r >= 0;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("type");
            }
        }

        public bool Check(Data data)
        {
            var firstArgument = _firstArgumentSelector.SelectData(data);
            var secondArgument = _secondArgumentSelector.SelectData(data);

            IComparer comparer = new Comparer(CultureInfo.CurrentCulture);
            int compareResult = comparer.Compare(firstArgument, secondArgument);
            return _checkComparisonResult(compareResult);
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(_firstArgumentSelector != null);
            Contract.Invariant(_secondArgumentSelector != null);
            Contract.Invariant(_checkComparisonResult != null);
        }
    }
}