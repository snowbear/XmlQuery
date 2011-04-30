using System.Collections.Generic;
using System.Diagnostics.Contracts;
using XmlQuery.Entities.Tokens;

namespace XmlQuery.Engine
{
    [ContractClass(typeof(IQueryTokenizerContracts))]
    public interface IQueryTokenizer
    {
        IEnumerable<Token> ParseTokens(string query);
    }

    // ReSharper disable InconsistentNaming
    [ContractClassFor(typeof(IQueryTokenizer))]
    public abstract class IQueryTokenizerContracts : IQueryTokenizer
    {
        public IEnumerable<Token> ParseTokens(string query)
        {
            Contract.Requires(query != null);
            return null;
        }
    }
    // ReSharper restore InconsistentNaming
}