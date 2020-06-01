using System.Linq;
using StackOverHead.Question.App.Models;
using StackOverHead.Question.Domain.Entities;

namespace StackOverHead.Question.App.Factories.Impl
{
    public class QuestionResponseFactory : IQuestionResponseFactory
    {
        public QuestionResponse Execute(QuestionEntity entity)
        {
            var response = new QuestionResponse
            {
                Id = entity.Id,
                Title = entity.Title,
                Body = entity.QuestionBody?.Body,
                Tags = entity.Tags,
                Votes = entity.QuestionBody.Votes,
                User = new UserResponse
                {
                    Id = entity.UserId,
                    Name = "Not especified yet"
                }
            };
            LoadAnswersToResponse(entity, response);

            LoadCommentsToResponse(entity, response);

            return response;
        }

        private void LoadAnswersToResponse(QuestionEntity entity, QuestionResponse response)
        {
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
                LoadAnswersCommentsToResponse(answer, newAnswer);

                response.Answers.Add(newAnswer);
            });
        }

        private void LoadAnswersCommentsToResponse(AnswerEntity entity, AnswerResponse response)
        {
            entity.Comments.ToList().ForEach(comment =>
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
                response.Comments.Add(newComment);
            });
        }

        private void LoadCommentsToResponse(QuestionEntity entity, QuestionResponse response)
        {
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
        }
    }
}