using System.Linq;
using StackOverHead.Question.App.Models;
using StackOverHead.Question.Domain.Entities;

namespace StackOverHead.Question.App.Factories.Impl
{
    public class QuestionResponseFactory : IQuestionResponseFactory
    {
        public QuestionResponse Execute(QuestionEntity from)
        {
            var response = new QuestionResponse
            {
                Id = from.Id,
                Title = from.Title,
                Body = from.QuestionBody?.Body,
                Tags = from.Tags,
                Votes = from.QuestionBody.Votes,
                User = new UserResponse
                {
                    Id = from.UserId,
                    Name = "Not especified yet"
                }
            };
            LoadAnswersToResponse(from, response);

            LoadCommentsToResponse(from, response);

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