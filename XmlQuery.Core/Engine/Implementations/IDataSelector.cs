using XmlQuery.Entities;

namespace XmlQuery.Engine.Implementations
{
    public interface IDataSelector
    {
        object SelectData(Data root);
    }
}