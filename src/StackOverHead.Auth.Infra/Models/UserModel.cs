using System;

namespace StackOverHead.Auth.Infra.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public byte[] Hash { get; set; }
        public byte[] Salt { get; set; }
        public string Roles { get; set; }
    }
}
