using StackOverHead.LibCommon.Entities;

namespace StackOverHead.LibCommon.Repositories
{
    public interface IConvertToEntity<TEntity, TData>
        where TEntity : Entity
        where TData : class
    {
        TEntity Execute(TData from);
    }
}