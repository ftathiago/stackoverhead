using System;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using StackOverHead.LibCommon.Repositories;
using StackOverHead.Question.Domain.Entities;
using StackOverHead.Question.Domain.Repositories;
using StackOverHead.Question.Infra.Context;
using StackOverHead.Question.Infra.Factories;
using StackOverHead.Question.Infra.Models;

namespace StackOverHead.Question.Infra.Repositories
{
    public class QuestionRepository : BaseRepository<QuestionEntity, QuestionModel>, IQuestionRepository
    {
        private readonly ICommentEntityToModelFactory _convertComment;

        public QuestionRepository(
            StackOverHeadQuestionDbContext dbContext,
            IQuestionEntityModelFactory convert,
            ICommentEntityToModelFactory convertComment)
            : base(dbContext, convert)
        {
            _convertComment = convertComment;
        }

        public async Task RegisterCommentAsync(CommentEntity comment)
        {
            var data = _convertComment.Execute(comment);
            await _context.Set<AnswerModel>().AddAsync(data);
            await _context.SaveChangesAsync();
        }

        protected override QuestionModel BeforePost(QuestionModel model, EntityState state)
        {
            if (state == EntityState.Added)
                model = SetCreatedAt(model);
            return model;
        }

        private QuestionModel SetCreatedAt(QuestionModel model)
        {
            model.CreatedAt = DateTime.Now;
            return model;
        }
    }
}