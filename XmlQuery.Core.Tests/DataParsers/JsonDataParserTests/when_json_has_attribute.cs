using Machine.Specifications;
using XmlQuery.Core.Tests.Extensions;
using XmlQuery.Entities;

namespace XmlQuery.Core.Tests.DataParsers.JsonDataParserTests
{
    public class when_json_has_attribute : JsonDataParserTestsBase
    {
        Because of = () => _result = _parser.Parse(@"{ value: '5' }");

        It should_not_return_null = () => _result.ShouldNotBeNull();
        It result_root_should_be_named_Data = () => _result.Name.ShouldEqual("Data");
        It result_root_should_be_of_type_Node = () => _result.ShouldBeOfType(typeof(Node));
        It should_parse_attribute_with_correct_type = () => _result.FirstChild().ShouldBeOfType(typeof(Attribute<string>));
        It should_parse_attribute_with_correct_value = () => _result.FirstChild().Cast<Attribute<string>>().Value.ShouldEqual("5");
    }
}