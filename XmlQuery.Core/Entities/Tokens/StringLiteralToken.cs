using System.Diagnostics.Contracts;

namespace XmlQuery.Entities.Tokens
{
    public class StringLiteralToken : Token
    {
        public string Value { get; private set; }

        public StringLiteralToken(string value)
        {
            Contract.Requires(value != null);

            Value = value;
        }

        public override string ToString()
        {
            return string.Format("StringLiteralToke(\"{0}\")", Value);
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(Value != null);
        }

    }
}