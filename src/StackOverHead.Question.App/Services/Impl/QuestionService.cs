using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using MediatR;
using StackOverHead.Question.App.Command;
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
            return _responseFactory.Execute(question);
        }

        public async Task<Guid> Add(AskQuestion request)
        {
            var command = new RegisterQuestionCommand
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Body = request.Body,
                Tags = request.Tags,
                UserId = request.UserId
            };
            await _mediator.Send(command);
            return command.Id;
        }

        public async Task<Guid> RegisterAnswer(AnswerRequest request)
        {
            var answerId = Guid.NewGuid();
            var command = new RegisterAnswerCommand
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

        public Task<IEnumerable<SearchQuestionResponse>> Search(
            string question,
            string tags,
            int page,
            int pageSize)
        {
            var questionCommand = new QuestionCommand(question, tags, page, pageSize);
            return _mediator.Send(questionCommand);
        }
    }
}