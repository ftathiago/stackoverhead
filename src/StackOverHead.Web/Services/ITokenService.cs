// <copyright file="ITokenService.cs" company="BlogDoFT">
// Copyright (c) BlogDoFT. All rights reserved.
// </copyright>

using StackOverHead.Auth.App.Models;

namespace StackOverHead.Web.Services
{
    public interface ITokenService
    {
        string GenerateToken(AuthenticatedUser user);
    }
}
