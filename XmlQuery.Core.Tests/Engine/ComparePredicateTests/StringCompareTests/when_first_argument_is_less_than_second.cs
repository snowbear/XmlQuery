using Machine.Specifications;
using XmlQuery.Engine.Implementations;

namespace XmlQuery.Core.Tests.Engine.ComparePredicateTests.StringCompareTests
{
    public class when_first_argument_is_less_than_second : ComparePredicateTestsBase
    {
        Establish context = () => SetCompareArguments("a", "b");

        It less_comparison_should_return_true = () => Assert(CompareType.Less, true);
        It less_or_equal_comparison_should_return_true = () => Assert(CompareType.LessOrEqual, true);
        It greater_comparison_should_return_false = () => Assert(CompareType.Greater, false);
        It greater_or_equal_comparison_should_return_false = () => Assert(CompareType.GreaterOrEqual, false);
    }
}