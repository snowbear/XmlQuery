using System.Diagnostics.Contracts;

namespace XmlQuery.Entities
{
    public abstract class Data
    {
        public string Name { get; private set; }

        protected Data(string name)
        {
            Contract.Requires(name != null);

            Name = name;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(Name != null);
        }

    }
}