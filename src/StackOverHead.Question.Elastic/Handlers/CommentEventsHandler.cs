using MediatR;
using StackOverHead.Question.Domain.Enums;
using StackOverHead.Question.Domain.Events;
using StackOverHead.Question.Elastic.Models;
using StackOverHead.Question.Elastic.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace StackOverHead.Question.Elastic.Handlers
{
    public class CommentEventsHandler :
        INotificationHandler<RegisteredQuestionComment>,
        INotificationHandler<RegisteredAnswerComment>
    {
        private readonly IAnswerRepository _answers;

        public CommentEventsHandler(IAnswerRepository answers)
        {
            _answers = answers;
        }

        public async Task Handle(RegisteredAnswerComment notification, CancellationToken cancellationToken)
        {
            var answer = new Answer
            {
                Id = notification.Id,
                QuestionId = notification.QuestionId,
                AnswerKind = AnswerKind.Comment,
                Content = notification.Content,
            };

            await _answers.AddAsync(answer);
        }

        public async Task Handle(RegisteredQuestionComment notification, CancellationToken cancellationToken)
        {
            var answer = new Answer
            {
                Id = notification.Id,
                QuestionId = notification.QuestionId,
                AnswerKind = AnswerKind.Comment,
                Content = notification.Content,
            };

            await _answers.AddAsync(answer);
        }
    }
}