namespace StackOverHead.LibCommon.Repositories
{
    public interface IEntityDTOConverter<TEntity, TDTO>
    {
        TDTO ToDTO(TEntity entity);
        TEntity ToEntity(TDTO data);
    }
}