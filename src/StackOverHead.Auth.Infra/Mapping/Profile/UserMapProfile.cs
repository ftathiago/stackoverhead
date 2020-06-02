using AutoMapper;
using StackOverHead.Auth.Domain.Entities;
using StackOverHead.Auth.Domain.ValueObjects;
using StackOverHead.Auth.Infra.Models;

namespace StackOverHead.Auth.Infra.Mapping.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserModel>()
                .ForMember(user => user.Hash, opt =>
                    opt.MapFrom(entity => entity.Password.Hash))
                .ForMember(model => model.Salt, opt =>
                    opt.MapFrom(entity => entity.Password.Salt));

            CreateMap<UserModel, User>()
                .ForMember(entity => entity.Password, opt =>
                    opt.MapFrom(model =>
                        new Password(model.Hash, model.Salt)
                    )
                );
        }
    }
}
