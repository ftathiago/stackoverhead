using System;

namespace StackOverHead.Question.App.Models
{
    public class AnswerRequest
    {
        public string Body { get; set; }
        public Guid UserId { get; set; }
    }
}