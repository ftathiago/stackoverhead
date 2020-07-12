using System;
using StackOverHead.Question.Domain.Enums;

namespace StackOverHead.Question.App.Models
{
    public class SearchQuestionResponse
    {
        public Guid Id { get; set; }
        public Guid QuestionId { get; set; }
        public AnswerKind AnswerKind { get; set; }
        public string Content { get; set; }
    }
}