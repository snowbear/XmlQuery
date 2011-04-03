using Machine.Specifications;
using XmlQuery.DataParsers;
using XmlQuery.Engine.Implementations;

namespace XmlQuery.Core.Tests.Engine.Implementations.PathExtractorTests
{
    public class when_data_can_be_extracted_by_path : PathExtractorTestsBase
    {
        private static object _result;

        Establish context = () => _sampleData = new JsonDataParser().Parse(@"{
                        'leaf': {
                            'attribute': 5
                        }
                    }
                ");

        Because of = () => _result = new PathExtractor().ExtractSingleValue(_sampleData, new[] {"Leaf", "attribute"});

        It should_extract_correct_value = () => _result.ShouldEqual(5);
    }
}