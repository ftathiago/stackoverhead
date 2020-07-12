using System;
using System.Collections.Generic;
using StackOverHead.Question.Domain.Enums;

namespace StackOverHead.Question.Elastic.Models
{
    public class Answer
    {
        public Guid Id { get; set; }
        public AnswerKind AnswerKind { get; set; }
        public Guid QuestionId { get; set; }
        public string Content { get; set; }
    }
}