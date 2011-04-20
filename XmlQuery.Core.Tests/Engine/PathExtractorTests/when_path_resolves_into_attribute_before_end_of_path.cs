using Machine.Specifications;
using XmlQuery.DataParsers;
using XmlQuery.Engine.Implementations;
using XmlQuery.Entities;

namespace XmlQuery.Core.Tests.Engine.PathExtractorTests
{
    public class when_path_resolves_into_attribute_before_end_of_path : PathExtractorTestsBase
    {
        private static object _result;

        Establish context = () => _sampleData = new JsonDataParser().Parse(@"{leaf: 5}");

        Because of = () => _result = new PathExtractor().ExtractSingleValue(_sampleData, new[] {"leaf", "attribute"});

        It should_return_DataNotFount = () => _result.ShouldEqual(DataNotFound.Instance);
    }
}