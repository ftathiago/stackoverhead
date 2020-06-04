using StackOverHead.LibCommon.Entities;

namespace StackOverHead.LibCommon.Repositories
{
    public interface IConvertFromEntity<TEntity, TData>
        where TEntity : Entity
        where TData : class
    {
        TData Execute(TEntity from);
    }
}