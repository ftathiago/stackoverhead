using System.Threading;
using System.Threading.Tasks;
using StackOverHead.Question.Domain.Events;
using MediatR;
using StackOverHead.Question.Elastic.Repositories;
using StackOverHead.Question.Elastic.Models;

namespace StackOverHead.Question.Elastic.EventSourcings
{
    public class ElasticEvents :
        INotificationHandler<RegisteredQuestion>,
        INotificationHandler<RegisteredQuestionComment>,
        INotificationHandler<RegisteredAnswer>,
        INotificationHandler<RegisteredAnswerComment>
    {
        private readonly IElasticRepository _elasticRepository;

        public ElasticEvents(IElasticRepository elasticRepository)
        {
            _elasticRepository = elasticRepository;
        }

        public async Task Handle(RegisteredQuestion notification, CancellationToken cancellationToken)
        {
            var question = new QuestionModel
            {
                Id = notification.Id,
                Title = notification.Title,
                Body = notification.Body,
                Date = notification.Date,
                Tags = notification.Tags,
            };
            await _elasticRepository.AddQuestionAsync(question);
        }

        public async Task Handle(RegisteredAnswer notification, CancellationToken cancellationToken)
        {
            var answer = new AnswerModel
            {
                Id = notification.Id,
                Content = notification.Content
            };

            await _elasticRepository.AddAnswerAsync(notification.QuestionId, answer);
        }

        public async Task Handle(RegisteredAnswerComment notification, CancellationToken cancellationToken)
        {
            var answerComment = new CommentModel
            {
                Id = notification.Id,
                Content = notification.Content,
                QuestionId = notification.QuestionId
            };

            await _elasticRepository.AddAnswerCommentAsync(
                notification.QuestionId,
                notification.AnswerId,
                answerComment);
        }

        public async Task Handle(RegisteredQuestionComment notification, CancellationToken cancellationToken)
        {
            var questionComment = new CommentModel
            {
                Id = notification.Id,
                QuestionId = notification.QuestionId,
                Content = notification.Content,
            };

            await _elasticRepository.AddQuestionCommentAsync(
                notification.QuestionId,
                questionComment);
        }
    }
}