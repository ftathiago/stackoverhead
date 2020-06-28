using System;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using StackOverHead.Question.Domain.Command;
using StackOverHead.Question.Domain.Entities;
using StackOverHead.Question.Domain.Enums;
using StackOverHead.Question.Domain.Events;
using StackOverHead.Question.Domain.Lib;
using StackOverHead.Question.Domain.Repositories;

namespace StackOverHead.Question.Domain.CommandHandlers
{
    public class QuestionCommandHandler : IRequestHandler<RegisterQuestionCommand, bool>
    {
        private readonly IQuestionRepository _repository;
        private readonly IQuestionEventLauncher _questionEvent;

        public QuestionCommandHandler(IQuestionRepository repository, IQuestionEventLauncher questionEvent)
        {
            _repository = repository;
            _questionEvent = questionEvent;
        }

        public async Task<bool> Handle(RegisterQuestionCommand request, CancellationToken cancellationToken)
        {
            var question = new QuestionEntity(
                request.Title,
                request.UserId,
                request.Tags
            );
            question.DefineId(request.Id);

            var questionBody = new AnswerEntity(
                request.Body,
                request.UserId,
                AnswerKind.QuestionBody,
                votes: 0
            );
            questionBody.DefineId(Guid.NewGuid());

            question.SetQuestionBody(questionBody);

            await _repository.RegisterAsync(question);

            await _questionEvent.Publish(question);

            return true;
        }
    }
}