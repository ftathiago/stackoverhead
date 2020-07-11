using System;
using System.Collections.Generic;

namespace StackOverHead.Question.Elastic.Models
{
    public class QuestionModel
    {
        public QuestionModel()
        {
            Comments = new List<CommentModel>();
            Answers = new List<AnswerModel>();
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime Date { get; set; }
        public string Tags { get; set; }
        public virtual List<CommentModel> Comments { get; set; }
        public virtual List<AnswerModel> Answers { get; set; }
    }
}