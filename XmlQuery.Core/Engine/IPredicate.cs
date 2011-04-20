using XmlQuery.Entities;

namespace XmlQuery.Engine
{
    public interface IPredicate
    {
        bool Check(Data data);
    }
}