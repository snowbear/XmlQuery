using Machine.Specifications;
using XmlQuery.DataParsers;
using XmlQuery.Entities;

namespace XmlQuery.Core.Tests.DataParsers.JsonDataParserTests
{
    public abstract class JsonDataParserTestsBase
    {
        protected static Data _result;
        protected static JsonDataParser _parser;

        Establish context = () => _parser = new JsonDataParser();
    }
}