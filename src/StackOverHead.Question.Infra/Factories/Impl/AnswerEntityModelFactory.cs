using System;
using System.Linq;
using StackOverHead.Question.Domain.Entities;
using StackOverHead.Question.Domain.Enums;
using StackOverHead.Question.Infra.Models;

namespace StackOverHead.Question.Infra.Factories.Impl
{
    public class AnswerEntityModelFactory : IAnswerEntityModelFactory
    {
        private readonly ICommentEntityToModelFactory _commentFactory;

        public AnswerEntityModelFactory(ICommentEntityToModelFactory commentFactory)
        {
            _commentFactory = commentFactory;
        }

        public AnswerModel ToDTO(AnswerEntity entity)
        {
            var model = new AnswerModel();
            model.Id = entity.Id;
            model.KindOf = (int)entity.Kind;
            model.QuestionId = entity.Parent.Id;
            model.Body = entity.Body;
            LoadCommentEntityToModel(entity, model);
            return model;
        }

        private void LoadCommentEntityToModel(AnswerEntity entity, AnswerModel model)
        {
            entity.Comments.ToList().ForEach(comment =>
            {
                var newComment = _commentFactory.ToDTO(comment);
                if (newComment.Id == Guid.Empty)
                    newComment.Id = Guid.NewGuid();
                model.Comments.Add(newComment);
            });
        }

        public AnswerEntity ToEntity(AnswerModel data)
        {
            var entity = new AnswerEntity(
                data.Body,
                data.UserId,
                (AnswerKind)data.KindOf
            );
            entity.DefineId(data.Id);
            data.Comments.ToList().ForEach(comment =>
            {
                var newComment = _commentFactory.ToEntity(comment);
                newComment.SetParent(entity);
                entity.AddComment(newComment);
            });
            return entity;
        }
    }
}