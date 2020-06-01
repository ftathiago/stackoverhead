using StackOverHead.LibCommon.Entities;

namespace StackOverHead.LibCommon.Repositories
{
    public interface IEntityDTOConverter<TEntity, TDTO> : IConvertToEntity<TEntity, TDTO>, IConvertFromEntity<TEntity, TDTO>
        where TEntity : Entity
        where TDTO : class
    { }
}