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
            model.UserId = from.UserId;
            ResolveParentId(model, from);
            return model;
        }

        private void ResolveParentId(AnswerModel model, CommentEntity from)
        {
            if (from.Parent is AnswerEntity)
                model.AnswerId = from.Parent.Id;
            if (from.Parent is QuestionEntity)
                model.QuestionId = from.Parent.Id;
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