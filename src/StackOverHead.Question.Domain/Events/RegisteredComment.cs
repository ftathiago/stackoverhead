using System;
using MediatR;

namespace StackOverHead.Question.Domain.Events
{
    public class RegisteredComment : INotification
    {
        public Guid Id { get; set; }
        public Guid QuestionId { get; set; }
        public Guid AnswerId { get; set; }
        public string Content { get; set; }
    }
}