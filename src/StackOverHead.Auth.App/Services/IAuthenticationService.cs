using StackOverHead.Auth.App.Models;
using StackOverHead.LibCommon.Services;

namespace StackOverHead.Auth.App.Services
{
    public interface IAuthenticationService : IServiceBase
    {
        AuthenticatedUser AuthenticateBy(string email, string password);
    }
}
