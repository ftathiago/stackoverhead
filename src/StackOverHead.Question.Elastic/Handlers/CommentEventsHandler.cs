using System.Threading;
using System.Threading.Tasks;
using StackOverHead.Question.Domain.Events;
using MediatR;
using StackOverHead.Question.Elastic.Repositories;
using StackOverHead.Question.Elastic.Models;
using StackOverHead.Question.Domain.Enums;

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