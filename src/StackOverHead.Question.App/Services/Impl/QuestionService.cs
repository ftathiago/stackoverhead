using System;
using System.Threading.Tasks;

using MediatR;

using StackOverHead.Question.App.Factories;
using StackOverHead.Question.App.Models;
using StackOverHead.Question.Domain.Command;
using StackOverHead.Question.Domain.Repositories;

namespace StackOverHead.Question.App.Services.Impl
{
    public class QuestionService : IQuestionService
    {
        private readonly IMediator _mediator;
        private readonly IQuestionRepository _repository;
        private readonly IQuestionResponseFactory _responseFactory;

        public QuestionService(
            IMediator mediator,
            IQuestionRepository repository,
            IQuestionResponseFactory responseFactory)
        {
            _mediator = mediator;
            _repository = repository;
            _responseFactory = responseFactory;
        }

        public async Task<QuestionResponse> GetById(Guid id)
        {
            var question = await _repository.GetByIdAsync(id);
            if (question == null)
                return new QuestionResponse();
            var response = _responseFactory.Execute(question);
            return response;
        }

        public async Task<Guid> Add(AskQuestion question)
        {
            var command = new AskQuestionCommand();
            command.Id = Guid.NewGuid();
            command.Title = question.Title;
            command.Body = question.Body;
            command.Tags = question.Tags;
            command.UserId = question.UserId;
            await _mediator.Send(command);
            return command.Id;
        }

        public async Task<Guid> RegisterAnswer(AnswerRequest request)
        {
            var answerId = Guid.NewGuid();
            var command = new AnswerCommand
            {
                Id = answerId,
                Body = request.Body,
                UserId = request.UserId,
                QuestionId = request.QuestionId
            };
            await _mediator.Send(command);
            return answerId;
        }

        public async Task<Guid> RegisterAnswerComment(AnswerCommentRequest request)
        {
            var commentId = Guid.NewGuid();
            var command = new RegisterAnswerCommentCommand
            {
                Id = commentId,
                Body = request.Body,
                UserId = request.UserId,
                QuestionId = request.QuestionId,
                AnswerId = request.AnswerId
            };
            await _mediator.Send(command);
            return commentId;
        }

        public async Task<Guid> RegisterQuestionComment(QuestionCommentRequest request)
        {
            var commentId = Guid.NewGuid();
            var command = new RegisterQuestionCommentCommand
            {
                Id = commentId,
                Body = request.Body,
                UserId = request.UserId,
                QuestionId = request.QuestionId,
            };

            await _mediator.Send(command);

            return commentId;
        }
    }
}