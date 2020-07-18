using System;
using System.Threading.Tasks;

using MediatR;

using StackOverHead.Question.Domain.Entities;
using StackOverHead.Question.Domain.Events;

namespace StackOverHead.Question.Domain.Lib.Impl
{
    public class QuestionEventLauncher : IQuestionEventLauncher
    {
        private readonly IMediator _mediator;

        public QuestionEventLauncher(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Publish(QuestionEntity askQuestion)
        {
            var registeredQuestion = new RegisteredQuestion
            {
                Id = askQuestion.Id,
                Title = askQuestion.Title,
                Body = askQuestion.QuestionBody.Body,
                Date = DateTime.Now,
                Tags = askQuestion.Tags
            };
            await _mediator.Publish<RegisteredQuestion>(registeredQuestion);
        }

        public async Task Publish(Guid questionId, Guid answerId, CommentEntity comment)
        {
            var registerComment = new RegisteredAnswerComment
            {
                Id = comment.Id,
                Content = comment.Body,
                QuestionId = questionId,
                AnswerId = answerId
            };
            await _mediator.Publish<RegisteredAnswerComment>(registerComment);
        }

        public async Task Publish(Guid questionId, CommentEntity comment)
        {
            var registerComment = new RegisteredQuestionComment
            {
                Id = comment.Id,
                Content = comment.Body,
                QuestionId = questionId,
            };
            await _mediator.Publish<RegisteredQuestionComment>(registerComment);
        }

        public async Task Publish(AnswerEntity answer)
        {
            var registeredAnswer = new RegisteredAnswer
            {
                Id = answer.Id,
                Content = answer.Body,
                QuestionId = answer.Parent.Id
            };
            await _mediator.Publish<RegisteredAnswer>(registeredAnswer);
        }
    }
}