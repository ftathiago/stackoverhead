using System;
using System.Linq;
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

        public async Task<QuestionResponse> GetById(Guid Id)
        {
            var question = await _repository.GetByIdAsync(Id);
            if (question == null)
                return new QuestionResponse();
            var response = _responseFactory.Execute(question);
            return response;
        }
    }
}