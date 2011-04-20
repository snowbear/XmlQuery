using System;
using Machine.Specifications;
using XmlQuery.DataParsers;
using XmlQuery.Engine.Implementations;

namespace XmlQuery.Core.Tests.Engine.PathExtractorTests
{
    public class when_data_by_path_is_ambiguous : PathExtractorTestsBase
    {
        private static Exception _exception;

        Establish context = () => _sampleData = new JsonDataParser().Parse(@"{ 'leafs': [5,6]}");

        Because of = () => _exception = Catch.Exception(() => new PathExtractor().ExtractSingleValue(_sampleData, new[] {"leafs", "Item"}));

        It should_throw_exception = () => _exception.ShouldNotBeNull();
    }
}