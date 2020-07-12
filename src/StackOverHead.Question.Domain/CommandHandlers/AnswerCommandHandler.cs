using System;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using StackOverHead.Question.Domain.Command;
using StackOverHead.Question.Domain.Entities;
using StackOverHead.Question.Domain.Enums;
using StackOverHead.Question.Domain.Lib;
using StackOverHead.Question.Domain.Repositories;

namespace StackOverHead.Question.Domain.CommandHandlers
{
    public class AnswerCommandHandler : IRequestHandler<RegisterAnswerCommand, bool>
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IAnswerRepository _answerRepository;
        private readonly IQuestionEventLauncher _questionEventLauncher;

        public AnswerCommandHandler(
            IQuestionRepository questionRepository,
            IAnswerRepository answerRepository,
            IQuestionEventLauncher questionEventLauncher)
        {
            _questionRepository = questionRepository;
            _answerRepository = answerRepository;
            _questionEventLauncher = questionEventLauncher;
        }

        public async Task<bool> Handle(RegisterAnswerCommand request, CancellationToken cancellationToken)
        {
            var question = _questionRepository.GetById(request.QuestionId);
            if (question == null)
            {
                //todo: Send error message
                return false;
            }

            var answer = new AnswerEntity(
                request.Body,
                request.UserId,
                AnswerKind.Answer,
                votes: 0);
            answer.DefineId(request.Id);
            answer.SetParent(question);

            await _answerRepository.RegisterAsync(answer);
            await _questionEventLauncher.Publish(answer);

            return true;
        }
    }
}