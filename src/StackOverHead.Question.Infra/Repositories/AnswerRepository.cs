using System.Threading.Tasks;
using StackOverHead.LibCommon.Repositories;
using StackOverHead.Question.Domain.Entities;
using StackOverHead.Question.Domain.Repositories;
using StackOverHead.Question.Infra.Context;
using StackOverHead.Question.Infra.Factories;
using StackOverHead.Question.Infra.Models;

namespace StackOverHead.Question.Infra.Repositories
{
    public class AnswerRepository : BaseRepository<AnswerEntity, AnswerModel>, IAnswerRepository
    {
        private readonly ICommentEntityToModelFactory _convertComment;

        public AnswerRepository(
            StackOverHeadQuestionDbContext dbContext,
            IAnswerEntityModelFactory convert,
            ICommentEntityToModelFactory convertComment)
            : base(dbContext, convert)
        {
            _convertComment = convertComment;
        }

        public async Task RegisterCommentAsync(CommentEntity comment)
        {
            var data = _convertComment.Execute(comment);
            await DbSet.AddAsync(data);
            await _context.SaveChangesAsync();
        }
    }
}