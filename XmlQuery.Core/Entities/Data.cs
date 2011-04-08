namespace XmlQuery.Entities
{
    public abstract class Data
    {
        public string Name { get; private set; }

        protected Data(string name)
        {
            Name = name;
        }
    }
}