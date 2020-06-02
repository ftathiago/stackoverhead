using System.Linq;

using StackOverHead.Auth.Domain.Entities;
using StackOverHead.Auth.Domain.Repositories;
using StackOverHead.Auth.Infra.Context;
using StackOverHead.Auth.Infra.Factories;
using StackOverHead.Auth.Infra.Models;
using StackOverHead.LibCommon.Repositories;

namespace StackOverHead.Auth.Infra.Repositories
{
    public class UserRepository : BaseRepository<User, UserModel>, IUserRepository
    {
        public UserRepository(StackOverHeadAuthDbContext dbContext, IUserEntityModelFactory factory)
          : base(dbContext, factory) { }
        public User GetByEmail(string email)
        {
            var user = DbSet.Where(user => user.Email == email).FirstOrDefault();
            User userEntity = _factory.Execute(user);
            return userEntity;
        }
    }
}
