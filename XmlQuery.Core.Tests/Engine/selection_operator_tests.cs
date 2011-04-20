using System.Collections.Generic;
using Machine.Specifications;
using Rhino.Mocks;
using XmlQuery.Engine;
using XmlQuery.Engine.Implementations;
using XmlQuery.Entities;
using System.Linq;

namespace XmlQuery.Core.Tests.Engine
{
    public class selection_operator_tests
    {
        private static IPredicate _predicate;
        private static SelectionOperator _selectionOperator;

        private static DataAttribute<int> _firstAttribute;
        private static DataAttribute<int> _secondAttribute;
        private static IEnumerable<Data> _result;

        Establish context = () =>
            {
                _predicate = MockRepository.GenerateMock<IPredicate>();
                _selectionOperator = new SelectionOperator(_predicate);

                _firstAttribute = new DataAttribute<int>("Attr1", 1);
                _secondAttribute = new DataAttribute<int>("Attr2", 2);

                _predicate.Expect(p => p.Check(_firstAttribute)).Return(false).Repeat.Any();
                _predicate.Expect(p => p.Check(_secondAttribute)).Return(true).Repeat.Any();
            };

        Because of = () => _result = _selectionOperator.ProcessData(new[] {_firstAttribute, _secondAttribute});

        It should_return_exactly_one_item = () => _result.Count().ShouldEqual(1);
        It should_not_return_first_attribute = () => _result.ShouldNotContain(_firstAttribute);
        It should_return_second_attribute = () => _result.ShouldContain(_secondAttribute);
    }
}