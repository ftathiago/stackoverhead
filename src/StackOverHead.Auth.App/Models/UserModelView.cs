using System;

namespace StackOverHead.Auth.App.Models
{
    public class UserModelView
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
    }
}