using System.Diagnostics.Contracts;

namespace XmlQuery.Entities
{
    public class DataAttribute<T> : DataAttribute
    {
        public DataAttribute(string name, T value) : base(name)
        {
            Contract.Requires(name != null);

            Value = value;
        }

        public T Value { get; private set; }

        public override object GetValue()
        {
            return Value;
        }
    }

    public abstract class DataAttribute : Data
    {
        protected DataAttribute(string name) : base(name)
        {
            Contract.Requires(name != null);
        }

        public abstract object GetValue();
    }
}