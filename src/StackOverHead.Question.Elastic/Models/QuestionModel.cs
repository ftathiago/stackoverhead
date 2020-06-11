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
        public string Body { get; set; }
        public DateTime Date { get; set; }
        public string Tags { get; set; }
        public List<CommentModel> Comments { get; private set; }
        public List<AnswerModel> Answers { get; private set; }
    }
}