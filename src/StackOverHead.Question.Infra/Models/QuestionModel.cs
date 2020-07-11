using System;
using System.Collections.Generic;

namespace StackOverHead.Question.Infra.Models
{
    public class QuestionModel
    {
        public QuestionModel()
        {
            Answers = new List<AnswerModel>();
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ViewCount { get; set; }
        public int Status { get; set; }
        public Guid UserId { get; set; }
        public string Tags { get; set; }
        public virtual ICollection<AnswerModel> Answers { get; set; }
    }
}