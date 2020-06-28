using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using StackOverHead.Question.Infra.Models;

namespace StackOverHead.Question.Infra.Mapping
{
    public class AnswerMap : IEntityTypeConfiguration<AnswerModel>
    {
        public void Configure(EntityTypeBuilder<AnswerModel> builder)
        {
            builder.ToTable("ANSWER");
            builder.HasKey(a => a.Id);
            builder
                .Property(a => a.Body)
                .HasMaxLength(int.MaxValue);
            builder
                .HasMany(a => a.Comments)
                .WithOne(c => c.Answer)
                .HasForeignKey(a => a.AnswerId);
        }
    }
}