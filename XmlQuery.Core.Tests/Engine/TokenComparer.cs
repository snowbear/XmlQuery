using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using XmlQuery.Entities.Tokens;

namespace XmlQuery.Core.Tests.Engine
{
    public class TokenComparer : IEqualityComparer<Token>
    {
        private static readonly Dictionary<Type, IEnumerable<Func<Token, object>>> CompareFieldExtractors = new Dictionary<Type, IEnumerable<Func<Token, object>>>
                                             {
                                                 {typeof (NumberLiteralToken), new Func<Token, object>[] {t => ((NumberLiteralToken) t).Value}},
                                                 {typeof (StringLiteralToken), new Func<Token, object>[] {t => ((StringLiteralToken) t).Value}},
                                                 {typeof (KeywordToken), new Func<Token, object>[] {t => ((KeywordToken) t).Keyword}},
                                                 {typeof (IdentifierToken), new Func<Token, object>[] {t => ((IdentifierToken) t).Identifier}},
                                             };

        public bool Equals(Token x, Token y)
        {
            if (x == null) return y == null;
            if (y == null) return false;

            if (x.GetType() != y.GetType()) return false;

            var typeSpecificCompareFieldExtractors = CompareFieldExtractors[x.GetType()];

            Contract.Assume(typeSpecificCompareFieldExtractors != null);

            return typeSpecificCompareFieldExtractors
                .All(f => Equals(f(x), f(y)));
        }

        public int GetHashCode(Token obj)
        {
            throw new NotSupportedException();
        }
    }
}