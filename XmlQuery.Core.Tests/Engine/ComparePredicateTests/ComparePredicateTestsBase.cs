using System;
using Machine.Specifications;
using Rhino.Mocks;
using XmlQuery.Engine.Implementations;
using XmlQuery.Entities;

namespace XmlQuery.Core.Tests.Engine.ComparePredicateTests
{
    public class ComparePredicateTestsBase
    {
        protected static IDataSelector _firstArgumentDataSelector;
        protected static IDataSelector _secondArgumentDataSelector;
        protected static ComparePredicate _comparePredicate;
        protected static object _firstArgument;
        protected static object _secondArgument;

        Establish context = () =>
            {
                _firstArgumentDataSelector = MockRepository.GenerateMock<IDataSelector>();
                _secondArgumentDataSelector = MockRepository.GenerateMock<IDataSelector>();
                _firstArgumentDataSelector.Expect(s => s.SelectData(Arg<Data>.Is.Anything)).Do(new Func<Data, object>(data => _firstArgument)).Repeat.Any();
                _secondArgumentDataSelector.Expect(s => s.SelectData(Arg<Data>.Is.Anything)).Do(new Func<Data, object>(data => _secondArgument)).Repeat.Any();
            };

        protected static void SetCompareArguments(object firstArgument, object secondArgument)
        {
            _firstArgument = firstArgument;
            _secondArgument = secondArgument;
        }

        protected static void Assert(CompareType compareType, bool expectedResult)
        {
            _comparePredicate = new ComparePredicate(_firstArgumentDataSelector, _secondArgumentDataSelector, compareType);
            _comparePredicate.Check(null).ShouldEqual(expectedResult);
        }
    }
}