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
    public class AnswerRepository : BaseRepository<AnswerEntity, AnswerModel>, IAnswerRepository
    {
        public AnswerRepository(StackOverHeadQuestionDbContext dbContext, IAnswerEntityModelFactory convert)
            : base(dbContext, convert)
        { }
    }
}