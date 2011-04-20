using Machine.Specifications;
using XmlQuery.Engine.Implementations;

namespace XmlQuery.Core.Tests.Engine.ComparePredicateTests.StringCompareTests
{
    public class when_first_argument_is_greater_than_second : ComparePredicateTestsBase
    {
        Establish context = () => SetCompareArguments("b", "a");

        It less_comparison_should_return_false = () => Assert(CompareType.Less, false);
        It less_or_equal_comparison_should_return_false = () => Assert(CompareType.LessOrEqual, false);
        It greater_comparison_should_return_true = () => Assert(CompareType.Greater, true);
        It greater_or_equal_comparison_should_return_true = () => Assert(CompareType.GreaterOrEqual, true);
    }
}