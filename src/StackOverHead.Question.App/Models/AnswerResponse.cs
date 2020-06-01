using System;
using System.Collections.Generic;

namespace StackOverHead.Question.App.Models
{
    public class AnswerResponse
    {
        public AnswerResponse()
        {
            Comments = new List<CommentResponse>();
        }
        public Guid Id { get; set; }
        public string Body { get; set; }
        public int Votes { get; set; }
        public UserResponse User { get; set; }
        public List<CommentResponse> Comments { get; set; }
    }
}