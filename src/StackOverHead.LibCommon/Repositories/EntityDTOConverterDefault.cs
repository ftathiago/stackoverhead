using AutoMapper;
using StackOverHead.LibCommon.Entities;

namespace StackOverHead.LibCommon.Repositories
{

    public class EntityDTOConverterDefault<TEntity, TDTO> : IEntityDTOConverter<TEntity, TDTO>
        where TEntity : Entity
        where TDTO : class
    {
        private readonly IMapper _mapper;

        public EntityDTOConverterDefault(IMapper mapper)
        {
            _mapper = mapper;
        }

        public TDTO Execute(TEntity from)
        {
            return _mapper.Map<TDTO>(from);
        }

        public TEntity Execute(TDTO from)
        {
            return _mapper.Map<TEntity>(from);
        }
    }
}