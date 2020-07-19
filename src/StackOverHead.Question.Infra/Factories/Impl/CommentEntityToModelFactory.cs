using StackOverHead.Question.Domain.Entities;
using StackOverHead.Question.Domain.Enums;
using StackOverHead.Question.Infra.Models;

namespace StackOverHead.Question.Infra.Factories.Impl
{
    public class CommentEntityToModelFactory : ICommentEntityToModelFactory
    {
        public AnswerModel Execute(CommentEntity from)
        {
            var model = new AnswerModel
            {
                KindOf = (int)AnswerKind.Comment,
                Id = from.Id,
                Body = from.Body,
                UserId = from.UserId
            };
            ResolveParentId(model, from);
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

        private void ResolveParentId(AnswerModel model, CommentEntity from)
        {
            if (from.Parent is AnswerEntity)
                model.AnswerId = from.Parent.Id;
            if (from.Parent is QuestionEntity)
                model.QuestionId = from.Parent.Id;
        }
    }
}