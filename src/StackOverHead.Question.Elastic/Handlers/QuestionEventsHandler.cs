using System.Threading;
using System.Threading.Tasks;
using MediatR;
using StackOverHead.Question.Domain.Enums;
using StackOverHead.Question.Domain.Events;
using StackOverHead.Question.Elastic.Models;
using StackOverHead.Question.Elastic.Repositories;

namespace StackOverHead.Question.Elastic.Handlers
{
    public class QuestionEventsHandler :
        INotificationHandler<RegisteredQuestion>
    {
        private readonly IAnswerRepository _answers;

        public QuestionEventsHandler(IAnswerRepository answers)
        {
            _answers = answers;
        }

        public async Task Handle(RegisteredQuestion notification, CancellationToken cancellationToken)
        {
            var answer = new Answer
            {
                Id = notification.Id,
                QuestionId = notification.Id,
                AnswerKind = AnswerKind.QuestionBody,
                Content = notification.Body,
            };
            await _answers.AddAsync(answer);
        }
    }
}