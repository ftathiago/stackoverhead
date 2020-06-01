using StackOverHead.Question.Domain.Entities;
using StackOverHead.Question.Domain.Enums;
using StackOverHead.Question.Infra.Models;

namespace StackOverHead.Question.Infra.Factories.Impl
{
    public class CommentEntityToModelFactory : ICommentEntityToModelFactory
    {
        public AnswerModel ToDTO(CommentEntity entity)
        {
            var model = new AnswerModel();
            model.KindOf = (int)AnswerKind.Comment;
            model.Id = entity.Id;
            model.Body = entity.Body;
            model.AnswerId = entity.Parent.Id;
            return model;
        }

        public CommentEntity ToEntity(AnswerModel data)
        {
            var entity = new CommentEntity(
                data.UserId,
                data.Body
            );
            entity.DefineId(data.Id);
            return entity;
        }
    }
}