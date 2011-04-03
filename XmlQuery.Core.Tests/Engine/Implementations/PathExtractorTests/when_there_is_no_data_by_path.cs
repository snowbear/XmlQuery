using Machine.Specifications;
using XmlQuery.DataParsers;
using XmlQuery.Engine.Implementations;
using XmlQuery.Entities;

namespace XmlQuery.Core.Tests.Engine.Implementations.PathExtractorTests
{
    public class when_there_is_no_data_by_path : PathExtractorTestsBase
    {
        private static object _result;

        Establish context = () => _sampleData = new JsonDataParser().Parse(@"{
                        'tree': {
                            'leafs': 5
                        }
                    }
                ");

        Because of = () => _result = new PathExtractor().ExtractSingleValue(_sampleData, new[] {"Leaf", "attribute"});

        It should_extract_correct_value = () => _result.ShouldEqual(DataNotFound.Instance);
    }
}