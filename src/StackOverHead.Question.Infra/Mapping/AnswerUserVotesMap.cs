using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using StackOverHead.Question.Infra.Models;

namespace StackOverHead.Question.Infra.Mapping
{
    public class AnswerUserVotesMap : IEntityTypeConfiguration<AnswerUserVotesModel>
    {
        public void Configure(EntityTypeBuilder<AnswerUserVotesModel> builder)
        {
            builder.ToTable("ANSWERUSERVOTES");
            builder.HasKey(au => new { au.AnswerId, au.UserId });
            builder
                .HasOne(au => au.Answer)
                .WithMany(answers => answers.UserVotes)
                .HasForeignKey(au => au.AnswerId);
        }
    }
}