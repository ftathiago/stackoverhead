using MediatR;
using StackOverHead.Question.Domain.Enums;
using StackOverHead.Question.Domain.Events;
using StackOverHead.Question.Elastic.Models;
using StackOverHead.Question.Elastic.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace StackOverHead.Question.Elastic.Handlers
{
    public class AnswerEventsHandler :
        INotificationHandler<RegisteredAnswer>
    {
        private readonly IAnswerRepository _answers;

        public AnswerEventsHandler(IAnswerRepository answers)
        {
            _answers = answers;
        }

        public async Task Handle(RegisteredAnswer notification, CancellationToken cancellationToken)
        {
            var answer = new Answer
            {
                Id = notification.Id,
                QuestionId = notification.QuestionId,
                AnswerKind = AnswerKind.Answer,
                Content = notification.Content
            };

            await _answers.AddAsync(answer);
        }
    }
}