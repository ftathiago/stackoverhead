using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StackOverHead.Question.Infra.Models;

namespace StackOverHead.Question.Infra.Mapping
{
    public class QuestionMap : IEntityTypeConfiguration<QuestionModel>
    {
        public void Configure(EntityTypeBuilder<QuestionModel> builder)
        {
            builder.ToTable("QUESTION");
            builder
                .HasKey(q => q.Id);
            builder
                .Property(q => q.Title)
                .HasMaxLength(300);
            builder
                .Property(q => q.Tags)
                .HasMaxLength(300);
            builder
                .HasMany(q => q.Answers)
                .WithOne(a => a.Question)
                .HasForeignKey(a => a.QuestionId);
        }
    }
}