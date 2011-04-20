using System.Linq;
using Machine.Specifications;
using XmlQuery.Engine.Implementations;
using XmlQuery.Entities;

namespace XmlQuery.Core.Tests.Engine.PathExtractorTests
{
    public class when_path_points_to_DataNode_instance : PathExtractorTestsBase
    {
        private static DataNode _rootNode;
        private static object _result;

        Establish context = () => _sampleData = new DataNode("Data", new[]
                                                                         {
                                                                             _rootNode = new DataNode("Root", Enumerable.Empty<Data>())
                                                                         });

        Because of = () => _result = new PathExtractor().ExtractSingleValue(_sampleData, new[] {"Root"});

        It should_return_node_itself = () => _result.ShouldBeTheSameAs(_rootNode);
    }
}