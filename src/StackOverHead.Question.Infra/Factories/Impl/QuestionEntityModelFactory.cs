using System;
using System.Linq;
using StackOverHead.Question.Domain.Entities;
using StackOverHead.Question.Domain.Enums;
using StackOverHead.Question.Infra.Models;

namespace StackOverHead.Question.Infra.Factories.Impl
{
    public class QuestionEntityModelFactory : IQuestionEntityModelFactory
    {
        private readonly IAnswerEntityModelFactory _answerFactory;
        private readonly ICommentEntityToModelFactory _commentFactory;

        public QuestionEntityModelFactory(
            IAnswerEntityModelFactory answerFactory,
            ICommentEntityToModelFactory commentFactory)
        {
            _answerFactory = answerFactory;
            _commentFactory = commentFactory;
        }

        public QuestionModel Execute(QuestionEntity from)
        {
            var model = new QuestionModel();
            model.Id = from.Id;
            model.Title = from.Title;
            model.UserId = from.UserId;
            model.Tags = from.Tags;
            LoadQuestionBodyFromEntityToModel(from, model);
            LoadAnswerCommentFromEntityToModel(from, model);
            return model;
        }


        public QuestionEntity Execute(QuestionModel from)
        {
            var entity = new QuestionEntity(
                from.Title,
                from.UserId,
                from.Tags
            );
            entity.DefineId(from.Id);
            LoadAnswerFromModelToEntity(entity, from);
            return entity;
        }

        private void LoadQuestionBodyFromEntityToModel(QuestionEntity entity, QuestionModel model)
        {
            if (entity.QuestionBody == null)
                return;
            var questionBody = new AnswerModel();
            questionBody.Id = entity.QuestionBody.Id;
            questionBody.Body = entity.QuestionBody.Body;
            questionBody.UserId = entity.QuestionBody.UserId;
            questionBody.KindOf = (int)AnswerKind.QuestionBody;
            model.Answers.Add(questionBody);
        }

        private void LoadAnswerCommentFromEntityToModel(QuestionEntity entity, QuestionModel model)
        {
            entity.Answers.ToList().ForEach(answer =>
            {
                var newAnswer = _answerFactory.Execute(answer);
                if (newAnswer.Id == Guid.Empty)
                    newAnswer.Id = Guid.NewGuid();
                model.Answers.Add(newAnswer);
            });
        }

        private void LoadAnswerFromModelToEntity(QuestionEntity entity, QuestionModel data)
        {
            data.Answers.ToList().ForEach(answer =>
            {
                var answerKind = (AnswerKind)answer.KindOf;
                switch (answerKind)
                {
                    case AnswerKind.Answer:
                        var newAnswer = _answerFactory.Execute(answer);
                        entity.AddAnswer(newAnswer);
                        break;
                    case AnswerKind.QuestionBody:
                        var questionBody = _answerFactory.Execute(answer);
                        entity.SetQuestionBody(questionBody);
                        break;
                    case AnswerKind.Comment:
                        var newComment = _commentFactory.Execute(answer);
                        entity.AddComment(newComment);
                        break;
                }
            });
        }
    }
}