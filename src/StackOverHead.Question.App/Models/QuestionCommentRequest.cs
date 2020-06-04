using System;

namespace StackOverHead.Question.App.Models
{
    public class QuestionCommentRequest
    {
        public Guid UserId { get; set; }
        public Guid QuestionId { get; set; }
        public string Body { get; set; }
    }
}