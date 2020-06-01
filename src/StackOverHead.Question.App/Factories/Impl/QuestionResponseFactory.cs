using System.Linq;
using StackOverHead.Question.App.Models;
using StackOverHead.Question.Domain.Entities;

namespace StackOverHead.Question.App.Factories.Impl
{
    public class QuestionResponseFactory : IQuestionResponseFactory
    {
        public QuestionResponse ToDTO(QuestionEntity entity)
        {
            var response = new QuestionResponse
            {
                Id = entity.Id,
                Title = entity.Title,
                Body = entity.QuestionBody?.Body,
                Tags = entity.Tags,
                Votes = entity.Votes,
                User = new UserResponse
                {
                    Id = entity.UserId,
                    Name = "Not especified yet"
                }
            };

            entity.Answers.ToList().ForEach(answer =>
            {
                var newAnswer = new AnswerResponse()
                {
                    Id = answer.Id,
                    Body = answer.Body,
                    Votes = answer.Votes,
                    User = new UserResponse
                    {
                        Id = answer.UserId
                    }
                };

                answer.Comments.ToList().ForEach(comment =>
                {
                    var newComment = new CommentResponse
                    {
                        Id = comment.Id,
                        Body = comment.Body,
                        User = new UserResponse
                        {
                            Id = comment.UserId
                        }
                    };
                    newAnswer.Comments.Add(newComment);
                });
                response.Answers.Add(newAnswer);
            });

            entity.Comments.ToList().ForEach(comment =>
            {
                var newComment = new CommentResponse()
                {
                    Id = comment.Id,
                    Body = comment.Body,
                    User = new UserResponse
                    {
                        Id = comment.UserId
                    }
                };
                response.Comments.Add(newComment);
            });

            return response;
        }

        public QuestionEntity ToEntity(QuestionResponse data)
        {
            throw new System.NotImplementedException();
        }
    }
}