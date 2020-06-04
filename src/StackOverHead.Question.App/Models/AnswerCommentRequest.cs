using System;
using System.ComponentModel.DataAnnotations;

namespace StackOverHead.Question.App.Models
{
    public class AnswerCommentRequest
    {
        [Required]
        public string Body { get; set; }
        public Guid UserId { get; set; }
        public Guid QuestionId { get; set; }
        public Guid AnswerId { get; set; }
    }
}