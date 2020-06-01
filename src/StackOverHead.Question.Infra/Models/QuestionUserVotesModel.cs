using System;

namespace StackOverHead.Question.Infra.Models
{
    public class QuestionUserVotesModel
    {
        public Guid QuestionId { get; set; }
        public virtual QuestionModel Question { get; set; }
        public Guid UserId { get; set; }
    }
}