using Microsoft.EntityFrameworkCore;
using StackOverHead.Question.Infra.Mapping;
using StackOverHead.Question.Infra.Models;

namespace StackOverHead.Question.Infra.Context
{
    public class StackOverHeadQuestionDbContext : DbContext
    {
        public DbSet<AnswerModel> Answers { get; set; }
        public DbSet<AnswerUserVotesModel> AnswerUserVotes { get; set; }
        public DbSet<QuestionModel> Questions { get; set; }
        public DbSet<QuestionUserVotesModel> QuestionUserVotes { get; set; }

        public StackOverHeadQuestionDbContext() : base() { }
        public StackOverHeadQuestionDbContext(DbContextOptions<StackOverHeadQuestionDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AnswerMap());
            modelBuilder.ApplyConfiguration(new QuestionMap());
            modelBuilder.ApplyConfiguration(new AnswerUserVotesMap());
            modelBuilder.ApplyConfiguration(new QuestionUserVotesMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}