using StackOverHead.LibCommon.Entities;

namespace StackOverHead.LibCommon.Repositories
{
    public interface IEntityDtoConverter<TEntity, TDTO> : IConvertToEntity<TEntity, TDTO>, IConvertFromEntity<TEntity, TDTO>
        where TEntity : Entity
        where TDTO : class
    { }
}