using System;

namespace StackOverHead.Question.Elastic.Models
{
    public class CommentModel
    {
        public Guid Id { get; set; }
        public Guid QuestionId { get; set; }
        public string Content { get; set; }
    }
}