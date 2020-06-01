using System;

namespace StackOverHead.Auth.App.Models
{
    public class AuthenticatedUser
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Roles { get; set; }
    }
}
