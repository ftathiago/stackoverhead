using System;

using StackOverHead.Auth.App.Models;
using StackOverHead.LibCommon.Services;

namespace StackOverHead.Auth.App.Services
{
    public interface IUserService : IServiceBase
    {
        UserModelView GetUserById(Guid id);
    }
}