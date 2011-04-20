using System;
using Machine.Specifications;
using Rhino.Mocks;
using XmlQuery.Engine.Implementations;

namespace XmlQuery.Core.Tests.Engine.ComparePredicateTests.StringCompareTests
{
    public class when_trying_to_create_predicate_with_unexpected_CompareType
    {
        private static Exception _exception;

        Because of = () => _exception = Catch.Exception(() => new ComparePredicate(MockRepository.GenerateMock<IDataSelector>(), MockRepository.GenerateMock<IDataSelector>(), (CompareType) int.MaxValue));

        It should_throw_ArgumentException = () => _exception.ShouldBeOfType<ArgumentException>();
    }
}