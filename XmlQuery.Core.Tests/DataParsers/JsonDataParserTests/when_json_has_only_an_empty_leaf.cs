using Machine.Specifications;
using XmlQuery.Core.Tests.Extensions;
using XmlQuery.Entities;

namespace XmlQuery.Core.Tests.DataParsers.JsonDataParserTests
{
    public class when_json_has_only_an_empty_leaf : JsonDataParserTestsBase
    {
        Because of = () => _result = _parser.Parse(@"{
                Leaf : { }
            }");

        It should_not_return_null = () => _result.ShouldNotBeNull();
        It result_root_should_be_named_Data = () => _result.Name.ShouldEqual("Data");
        It result_root_should_be_of_type_Node = () => _result.ShouldBeOfType(typeof(DataNode));
        It should_parse_leaf_with_correct_type = () => _result.FirstChild().ShouldBeOfType(typeof(DataNode));
        It should_parse_leaf_with_correct_name = () => _result.FirstChild().Name.ShouldEqual("Leaf");
        It should_parse_leaf_with_no_children = () => _result.FirstChild().Cast<DataNode>().Children.ShouldBeEmpty();
    }
}