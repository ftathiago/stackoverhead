using System;
using System.ComponentModel.DataAnnotations;

namespace StackOverHead.Question.App.Models
{
    public class AnswerRequest
    {
        [Required]
        public string Body { get; set; }
        public Guid UserId { get; set; }
    }
}