using System;
using System.Collections.Generic;

namespace StackOverHead.Question.App.Models
{
    public class QuestionResponse
    {
        public QuestionResponse()
        {
            Answers = new List<AnswerResponse>();
            Comments = new List<CommentResponse>();
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public UserResponse User { get; set; }
        public string Tags { get; set; }
        public int Votes { get; set; }
        public List<CommentResponse> Comments { get; set; }
        public List<AnswerResponse> Answers { get; set; }
    }
}