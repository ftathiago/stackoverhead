using StackOverHead.Auth.App.Models;

namespace StackOverHead.Web.Services
{
    public interface ITokenService
    {
        string GenerateToken(AuthenticatedUser user);
    }
}
