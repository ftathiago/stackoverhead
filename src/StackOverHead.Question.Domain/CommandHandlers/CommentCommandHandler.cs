using MediatR;
using StackOverHead.Question.Domain.Command;
using StackOverHead.Question.Domain.Entities;
using StackOverHead.Question.Domain.Lib;
using StackOverHead.Question.Domain.Repositories;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StackOverHead.Question.Domain.CommandHandlers
{
    public class CommentCommandHandler :
        IRequestHandler<RegisterAnswerCommentCommand, bool>,
        IRequestHandler<RegisterQuestionCommentCommand, bool>
    {
        private readonly IAnswerRepository _repository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IQuestionEventLauncher _questionEventLauncher;

        public CommentCommandHandler(
            IAnswerRepository repository,
            IQuestionRepository questionRepository,
            IQuestionEventLauncher questionEventLauncher)
        {
            _repository = repository;
            _questionRepository = questionRepository;
            _questionEventLauncher = questionEventLauncher;
        }

        public async Task<bool> Handle(RegisterAnswerCommentCommand request, CancellationToken cancellationToken)
        {
            var question = await _questionRepository.GetByIdAsync(request.QuestionId);
            if (question == null)
            {
                return false;
            }

            var answer = question.Answers.ToList().Find(answer => answer.Id == request.AnswerId);
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
            await _questionEventLauncher.Publish(request.QuestionId, request.AnswerId, comment);

            return true;
        }

        public async Task<bool> Handle(RegisterQuestionCommentCommand request, CancellationToken cancellationToken)
        {
            var question = await _questionRepository.GetByIdAsync(request.QuestionId);
            if (question == null)
            {
                return false;
            }

            var comment = new CommentEntity(
                request.UserId,
                request.Body
            );

            comment.DefineId(request.Id);
            comment.SetParent(question);

            await _questionRepository.RegisterCommentAsync(comment);
            await _questionEventLauncher.Publish(request.QuestionId, comment);

            return true;
        }
    }
}