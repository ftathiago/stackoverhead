using System;
using System.Collections.Generic;

namespace StackOverHead.Question.Infra.Models
{
    public class AnswerModel
    {
        public Guid Id { get; set; }
        public int KindOf { get; set; }
        public string Body { get; set; }
        public Guid UserId { get; set; }
        public Guid? AnswerId { get; set; }
        public Guid? QuestionId { get; set; }
        public int Votes { get; set; }
        public virtual ICollection<AnswerUserVotesModel> UserVotes { get; private set; }
        public virtual ICollection<AnswerModel> Comments { get; private set; }
        public virtual AnswerModel Answer { get; set; }
        public virtual QuestionModel Question { get; set; }
    }
}