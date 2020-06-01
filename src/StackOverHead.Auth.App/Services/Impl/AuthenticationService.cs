using StackOverHead.Auth.App.Models;
using StackOverHead.Auth.App.Services;
using StackOverHead.Auth.Domain.Entities;
using StackOverHead.Auth.Domain.Repositories;
using StackOverHead.Auth.Domain.ValueObjects;
using StackOverHead.LibCommon.Services;

namespace StackOverHead.Auth.App.Services.Impl
{
    public class AuthenticationService : ServiceBase, IAuthenticationService
    {
        private const string CREDENTIALS_ERRORS = "User or password is invalid";
        private readonly IUserRepository _userRepository;

        public AuthenticationService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public AuthenticatedUser AuthenticateBy(string email, string password)
        {
            var user = _userRepository.GetByEmail(email);

            if (!ValidateCredentials(user, password))
            {
                return null;
            }

            var userAuthenticateResponse = BuildAuthenticatedUser(user);

            return userAuthenticateResponse;
        }

        private bool ValidateCredentials(User user, string password)
        {
            if (user == null)
            {
                AddErrorMessage(CREDENTIALS_ERRORS);
                return false;
            }

            var credentials = new Credentials(user.Email,
                password, user.Password.Salt);

            if (!user.IsValidCredentials(credentials))
            {
                AddErrorMessage(CREDENTIALS_ERRORS);
                return false;
            }
            return true;
        }

        private AuthenticatedUser BuildAuthenticatedUser(User user)
        {
            var userAuthenticateResponse = new AuthenticatedUser
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Roles = user.Roles
            };
            return userAuthenticateResponse;
        }
    }
}
