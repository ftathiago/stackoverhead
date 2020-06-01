
using StackOverHead.Auth.Domain.Entities;
using StackOverHead.LibCommon.Repositories;

namespace StackOverHead.Auth.Domain.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        User GetByEmail(string email);
    }
}
