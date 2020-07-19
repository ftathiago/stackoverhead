using StackOverHead.LibCommon.Entities;

namespace StackOverHead.LibCommon.Repositories
{
    public interface IConvertToEntity<out TEntity, in TData>
        where TEntity : Entity
        where TData : class
    {
        TEntity Execute(TData from);
    }
}