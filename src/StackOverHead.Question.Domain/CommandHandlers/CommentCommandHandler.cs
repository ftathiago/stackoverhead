using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using StackOverHead.Question.Domain.Command;
using StackOverHead.Question.Domain.Entities;
using StackOverHead.Question.Domain.Repositories;

namespace StackOverHead.Question.Domain.CommandHandlers
{
    public class CommentCommandHandler
        : IRequestHandler<RegisterAnswerCommentCommand, bool>
    {
        private readonly IAnswerRepository _repository;
        private readonly IQuestionRepository _questionRepository;

        public CommentCommandHandler(
            IAnswerRepository repository,
            IQuestionRepository questionRepository)
        {
            _repository = repository;
            _questionRepository = questionRepository;
        }
        public async Task<bool> Handle(RegisterAnswerCommentCommand request, CancellationToken cancellationToken)
        {
            var question = await _questionRepository.GetByIdAsync(request.QuestionId);
            if (question == null)
            {
                return false;
            }

            var answer = question.Answers.ToList().FirstOrDefault(answer => answer.Id == request.AnswerId);
            if (answer == null)
            {
                return false;
            }

            var comment = new CommentEntity(
                request.UserId,
                request.Body
            );

            comment.DefineId(request.Id);
            comment.SetParent(answer);

            await _repository.RegisterCommentAsync(comment);

            return true;
        }
    }
}