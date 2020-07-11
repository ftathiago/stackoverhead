using AutoMapper;

using StackOverHead.Auth.Domain.Entities;
using StackOverHead.Auth.Infra.Models;
using StackOverHead.LibCommon.Repositories;

namespace StackOverHead.Auth.Infra.Factories
{
    public interface IUserEntityModelFactory : IEntityDtoConverter<User, UserModel>
    { }

    public class UserEntityModelFactory :
        EntityDtoConverterDefault<User, UserModel>,
        IUserEntityModelFactory
    {
        public UserEntityModelFactory(IMapper mapper) : base(mapper)
        { }
    }
}