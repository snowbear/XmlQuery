using System.Diagnostics.Contracts;

namespace XmlQuery.Entities.Tokens
{
    public class IdentifierToken : Token
    {
        public string Identifier { get; private set; }

        public IdentifierToken(string identifier)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(identifier));

            Identifier = identifier;
        }

        [ContractInvariantMethod]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Required for code contracts.")]
        private void ObjectInvariant()
        {
            Contract.Invariant(!string.IsNullOrWhiteSpace(Identifier));
        }
    }
}