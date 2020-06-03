using StackOverHead.Question.Domain.Entities;
using StackOverHead.Question.Domain.Enums;
using StackOverHead.Question.Infra.Models;

namespace StackOverHead.Question.Infra.Factories.Impl
{
    public class CommentEntityToModelFactory : ICommentEntityToModelFactory
    {
        public AnswerModel Execute(CommentEntity from)
        {
            var model = new AnswerModel();
            model.KindOf = (int)AnswerKind.Comment;
            model.Id = from.Id;
            model.Body = from.Body;
            model.AnswerId = from.Parent.Id;
            model.UserId = from.UserId;
            return model;
        }

        public CommentEntity Execute(AnswerModel from)
        {
            var entity = new CommentEntity(
                from.UserId,
                from.Body
            );
            entity.DefineId(from.Id);
            return entity;
        }
    }
}