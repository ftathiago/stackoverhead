using System.Threading;
using System.Threading.Tasks;
using StackOverHead.Question.Domain.Events;
using MediatR;
using StackOverHead.Question.Elastic.Repositories;
using StackOverHead.Question.Elastic.Models;
using StackOverHead.Question.Domain.Enums;

namespace StackOverHead.Question.Elastic.EventSourcings
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