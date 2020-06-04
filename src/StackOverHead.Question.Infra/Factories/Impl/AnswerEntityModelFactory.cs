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

        public AnswerModel Execute(AnswerEntity from)
        {
            var model = new AnswerModel();
            model.Id = from.Id;
            model.KindOf = (int)from.Kind;
            model.QuestionId = from.Parent.Id;
            model.Body = from.Body;
            model.UserId = from.UserId;
            LoadCommentEntityToModel(from, model);
            return model;
        }

        public AnswerEntity Execute(AnswerModel from)
        {
            var entity = new AnswerEntity(
                from.Body,
                from.UserId,
                (AnswerKind)from.KindOf,
                from.Votes
            );
            entity.DefineId(from.Id);
            from.Comments.ToList().ForEach(comment =>
            {
                var newComment = _commentFactory.Execute(comment);
                newComment.SetParent(entity);
                entity.AddComment(newComment);
            });
            return entity;
        }

        private void LoadCommentEntityToModel(AnswerEntity entity, AnswerModel model)
        {
            entity.Comments.ToList().ForEach(comment =>
            {
                var newComment = _commentFactory.Execute(comment);
                if (newComment.Id == Guid.Empty)
                    newComment.Id = Guid.NewGuid();
                model.Comments.Add(newComment);
            });
        }
    }
}