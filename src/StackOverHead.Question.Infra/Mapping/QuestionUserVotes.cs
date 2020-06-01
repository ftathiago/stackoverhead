using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StackOverHead.Question.Infra.Models;

namespace StackOverHead.Question.Infra.Mapping
{
    public class QuestionUserVotesMap : IEntityTypeConfiguration<QuestionUserVotesModel>
    {
        public void Configure(EntityTypeBuilder<QuestionUserVotesModel> builder)
        {
            builder.ToTable("QUESTIONUSERVOTES");
            builder.HasKey(qu => new { qu.QuestionId, qu.UserId });
            builder
                .HasOne(qu => qu.Question)
                .WithMany(question => question.UserVotes)
                .HasForeignKey(qu => qu.QuestionId);
        }
    }
}