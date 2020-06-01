using System;
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
        public QuestionRepository(StackOverHeadQuestionDbContext dbContext, IQuestionEntityModelFactory convert)
            : base(dbContext, convert)
        { }
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