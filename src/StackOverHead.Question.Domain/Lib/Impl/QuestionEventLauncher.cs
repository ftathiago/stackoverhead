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

        public async Task Publish(QuestionEntity question)
        {
            var registeredQuestion = new RegisteredQuestion
            {
                Id = question.Id,
                Body = question.QuestionBody.Body,
                Date = DateTime.Now,
                Tags = question.Tags
            };
            await _mediator.Publish<RegisteredQuestion>(registeredQuestion);
        }

        public async Task Publish(Guid questionId, Guid answerId, CommentEntity comment)
        {
            var registerComment = new RegisteredComment
            {
                Id = comment.Id,
                Content = comment.Body,
                QuestionId = questionId,
                AnswerId = answerId
            };
            await _mediator.Publish<RegisteredComment>(registerComment);
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