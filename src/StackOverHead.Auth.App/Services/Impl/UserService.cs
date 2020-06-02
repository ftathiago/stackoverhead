using System;
using StackOverHead.Auth.App.Models;
using StackOverHead.Auth.Domain.Repositories;
using StackOverHead.LibCommon.Services;

namespace StackOverHead.Auth.App.Services.Impl
{
    public class UserService : ServiceBase, IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }
        public UserModelView GetUserById(Guid id)
        {
            var user = _repository.GetById(id);
            if (user == null)
                return new UserModelView();
            return new UserModelView
            {
                Id = user.Id,
                FullName = user.Name,
                Email = user.Email
            };
        }
    }
}