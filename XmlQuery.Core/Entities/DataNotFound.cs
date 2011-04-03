namespace XmlQuery.Entities
{
    public sealed class DataNotFound
    {
        public static readonly DataNotFound Instance = new DataNotFound();

        private DataNotFound()
        {
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null)) return false;
            return obj.GetType() == typeof(DataNotFound);
        }

        public override int GetHashCode()
        {
            return 0;
        }
    }
}