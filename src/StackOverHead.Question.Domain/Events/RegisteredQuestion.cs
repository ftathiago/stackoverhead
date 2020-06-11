using System;
using MediatR;

namespace StackOverHead.Question.Domain.Events
{
    public class RegisteredQuestion : INotification
    {
        public Guid Id { get; set; }
        public string Body { get; set; }
        public DateTime Date { get; set; }
        public string Tags { get; set; }
    }
}