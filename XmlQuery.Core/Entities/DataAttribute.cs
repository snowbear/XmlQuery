namespace XmlQuery.Entities
{
    public class DataAttribute<T> : DataAttribute
    {
        public DataAttribute(string name, T value) : base(name)
        {
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
        }

        public abstract object GetValue();
    }
}