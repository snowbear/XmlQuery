using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using XmlQuery.Entities.Tokens;

namespace XmlQuery.Engine.Implementations
{
    public class QueryTokenizer : IQueryTokenizer
    {
        private static readonly IEnumerable<Func<string, int, Tuple<Token, int>>> ParsingDelegates =
            new Func<string, int, Tuple<Token, int>>[]
                {
                    TryParseWhiteSpace,
                    TryParseStringLiteral,
                    TryParseNumberLiteral,
                    TryParseIdentifierOrKeyword,
                };

        public IEnumerable<Token> ParseTokens(string query)
        {
            var tokens = new List<Token>();
            var nextCharIndexToParse = 0;
            while (nextCharIndexToParse < query.Length)
            {
                var parsingResult = ParsingDelegates
                    .Select(parser => parser(query, nextCharIndexToParse))
                    .FirstOrDefault(r => r != null);

                if (parsingResult == null)
                    throw new NotSupportedException(string.Format("Unexpected character at position {0}: '{1}'", nextCharIndexToParse, query[nextCharIndexToParse]));

                if (parsingResult.Item1 != null)
                    tokens.Add(parsingResult.Item1);

                nextCharIndexToParse = parsingResult.Item2;
                Contract.Assume(nextCharIndexToParse > 0);

            }
            return tokens;
        }

        private static Tuple<Token, int> TryParseWhiteSpace(string query, int indexToParse)
        {
            ContractParserAbbreaviation(query, indexToParse);

            return char.IsWhiteSpace(query, indexToParse)
                       ? new Tuple<Token, int>(null, indexToParse + 1)
                       : null;
        }

        private static Tuple<Token, int> TryParseStringLiteral(string query, int indexToParse)
        {
            ContractParserAbbreaviation(query, indexToParse);

            if (query[indexToParse] != '"' && query[indexToParse] != '\'')
                return null;

            if (indexToParse == query.Length - 1)
                throw new Exception("Closing quote char not found");

            var indexOfClosingChar = query.IndexOf(query[indexToParse], indexToParse + 1);
            if (indexOfClosingChar == -1)
                throw new Exception("Closing quote char not found");

            Contract.Assume(indexOfClosingChar > indexToParse);

            var stringLiteralValue = query.Substring(indexToParse + 1, indexOfClosingChar - indexToParse - 1);
            var stringLiteral = new StringLiteralToken(stringLiteralValue);

            return new Tuple<Token, int>(stringLiteral, indexOfClosingChar + 1);
        }

        private static Tuple<Token, int> TryParseNumberLiteral(string query, int indexToParse)
        {
            ContractParserAbbreaviation(query, indexToParse);

            if (!char.IsDigit(query, indexToParse))
                return null;

            var numberCharactersCount = query
                .Skip(indexToParse)
                .TakeWhile(c => char.IsDigit(c) || c == '.')
                .Count();

            Contract.Assume(numberCharactersCount > 0);
            Contract.Assume(numberCharactersCount < query.Length - indexToParse);

            var numberLiteralValue = double.Parse(query.Substring(indexToParse, numberCharactersCount), NumberStyles.AllowDecimalPoint);
            var numberLiteral = new NumberLiteralToken(numberLiteralValue);
            return new Tuple<Token, int>(numberLiteral, indexToParse + numberCharactersCount);
        }

        private static Tuple<Token, int> TryParseIdentifierOrKeyword(string query, int indexToParse)
        {
            ContractParserAbbreaviation(query, indexToParse);

            if (!char.IsLetter(query, indexToParse) && query[indexToParse] != '_')
                return null;

            var wordCharactersCount = query
                .Skip(indexToParse)
                .TakeWhile(c => char.IsLetterOrDigit(c) || c == '_')
                .Count();

            Contract.Assume(wordCharactersCount > 0);
            Contract.Assume(wordCharactersCount < query.Length - indexToParse);

            var word = query.Substring(indexToParse, wordCharactersCount);

            Keyword keyword;
            var token = Enum.TryParse(word, true, out keyword)
                            ? (Token)new KeywordToken(keyword)
                            : new IdentifierToken(word);

            return new Tuple<Token, int>(token, indexToParse + wordCharactersCount);
        }

        [ContractAbbreviator]
        private static void ContractParserAbbreaviation(string query, int indexToParse)
        {
            Contract.Requires(query != null);
            Contract.Requires(indexToParse >= 0);
            Contract.Requires(indexToParse < query.Length);

            Contract.Ensures(Contract.Result<Tuple<Token,int>>() == null ||
                Contract.Result<Tuple<Token, int>>().Item2 > indexToParse);
            Contract.Ensures(Contract.Result<Tuple<Token,int>>() == null ||
                Contract.Result<Tuple<Token, int>>().Item2 > 0);
        }
    }
}