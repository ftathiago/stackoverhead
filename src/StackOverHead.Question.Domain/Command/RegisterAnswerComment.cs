using System;
using MediatR;

namespace StackOverHead.Question.Domain.Command
{
    public class RegisterAnswerCommentCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public Guid QuestionId { get; set; }
        public Guid AnswerId { get; set; }
        public Guid UserId { get; set; }
        public string Body { get; set; }
    }
}