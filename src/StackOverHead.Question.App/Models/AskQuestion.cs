using System;

namespace StackOverHead.Question.App.Models
{
    public class AskQuestion
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public Guid UserId { get; set; }
        public string Tags { get; set; }
    }
}