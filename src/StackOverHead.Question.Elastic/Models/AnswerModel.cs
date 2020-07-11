using System;
using System.Collections.Generic;

namespace StackOverHead.Question.Elastic.Models
{
    public class AnswerModel
    {
        public AnswerModel()
        {
            Comments = new List<CommentModel>();
        }

        public Guid Id { get; set; }
        public Guid QuestionId { get; set; }
        public string Content { get; set; }
        public IList<CommentModel> Comments { get; set; }
    }
}