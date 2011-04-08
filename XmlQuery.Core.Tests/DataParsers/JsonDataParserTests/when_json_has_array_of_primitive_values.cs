using Machine.Specifications;
using XmlQuery.Core.Tests.Extensions;
using XmlQuery.Entities;

namespace XmlQuery.Core.Tests.DataParsers.JsonDataParserTests
{
    public class when_json_has_array_of_primitive_values : JsonDataParserTestsBase
    {
        Because of = () => _result = _parser.Parse(@"{ leafs : [1,2] }");

        It should_parse_all_array_items_with_correct_types = () => _result.FirstChild().Cast<DataNode>().Children.ShouldEachConformTo(t => t.GetType() == typeof (DataAttribute<int>));
        It should_parse_all_array_items_with_correct_names = () => _result.FirstChild().Cast<DataNode>().Children.ShouldEachConformTo(t => t.Name == "Item");
        It should_parse_first_array_item_with_correct_value = () => _result.FirstChild().NthChild(0).Cast<DataAttribute<int>>().Value.ShouldEqual(1);
        It should_parse_second_array_item_with_correct_value = () => _result.FirstChild().NthChild(1).Cast<DataAttribute<int>>().Value.ShouldEqual(2);
    }
}