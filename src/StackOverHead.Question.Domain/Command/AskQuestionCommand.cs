using System;
using MediatR;

namespace StackOverHead.Question.Domain.Command
{
    public class AskQuestionCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public Guid UserId { get; set; }
        public string Tags { get; set; }
    }
}