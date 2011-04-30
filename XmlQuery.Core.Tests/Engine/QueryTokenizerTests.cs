using NUnit.Framework;
using XmlQuery.Core.Tests.Extensions;
using XmlQuery.Engine.Implementations;
using XmlQuery.Entities.Tokens;

namespace XmlQuery.Core.Tests.Engine
{
    [TestFixture]
    public class QueryTokenizerTests
    {
        [Test]
        public void ParseTokensTests()
        {
            var @is = new Is();
            new[]
                {
                    TestData.Create("  ", new Token[0]),
                    TestData.Create("'Hello'", new Token[] {new StringLiteralToken("Hello")}),
                    TestData.Create("\"Hello\"", new Token[] {new StringLiteralToken("Hello")}),

                    TestData.Create("15", new Token[] {new NumberLiteralToken(15)}),
                    TestData.Create("1.4", new Token[] {new NumberLiteralToken(1.4)}),

                    TestData.Create("select", new Token[] {new KeywordToken(Keyword.Select)}),
                    TestData.Create("SELECT", new Token[] {new KeywordToken(Keyword.Select)}),
                    TestData.Create("select from", new Token[] {new KeywordToken(Keyword.Select), new KeywordToken(Keyword.From)}),

                    TestData.Create("hello", new Token[] {new IdentifierToken("hello")}),
                    TestData.Create("a1", new Token[] {new IdentifierToken("a1")}),
                    TestData.Create("_a", new Token[] {new IdentifierToken("_a")}),
                }.AssertDataDrivenTest(
                    s => new QueryTokenizer().ParseTokens(s),
                    _ => Is.Not.Null,
                    expected => @is.EquivalentTo(expected, new TokenComparer()));
        }
    }
}