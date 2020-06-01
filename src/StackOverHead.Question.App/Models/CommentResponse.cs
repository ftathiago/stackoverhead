using System;

namespace StackOverHead.Question.App.Models
{
    public class CommentResponse
    {
        public Guid Id { get; set; }
        public string Body { get; set; }
        public UserResponse User { get; set; }
    }
}