using System;

namespace StackOverHead.Question.Infra.Models
{
    public class AnswerUserVotesModel
    {
        public Guid AnswerId { get; set; }
        public virtual AnswerModel Answer { get; set; }
        public Guid UserId { get; set; }
    }
}