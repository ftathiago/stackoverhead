using System;

using MediatR;

namespace StackOverHead.Question.Domain.Command
{
    public class RegisterAnswerCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid QuestionId { get; set; }
        public string Body { get; set; }
    }
}