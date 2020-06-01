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

        public QuestionModel ToDTO(QuestionEntity entity)
        {
            var model = new QuestionModel();
            model.Id = entity.Id;
            model.Title = entity.Title;
            model.UserId = entity.UserId;
            model.Tags = entity.Tags;
            LoadQuestionBodyFromEntityToModel(entity, model);
            LoadAnswerCommentFromEntityToModel(entity, model);
            return model;
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
                var newAnswer = _answerFactory.ToDTO(answer);
                if (newAnswer.Id == Guid.Empty)
                    newAnswer.Id = Guid.NewGuid();
                model.Answers.Add(newAnswer);
            });
        }

        public QuestionEntity ToEntity(QuestionModel data)
        {
            var entity = new QuestionEntity(
                data.Title,
                data.UserId,
                data.Tags
            );
            entity.DefineId(data.Id);
            LoadAnswerFromModelToEntity(entity, data);
            return entity;
        }

        private void LoadAnswerFromModelToEntity(QuestionEntity entity, QuestionModel data)
        {
            data.Answers.ToList().ForEach(answer =>
            {
                var answerKind = (AnswerKind)answer.KindOf;
                switch (answerKind)
                {
                    case AnswerKind.Answer:
                        var newAnswer = _answerFactory.ToEntity(answer);
                        entity.AddAnswer(newAnswer);
                        break;
                    case AnswerKind.QuestionBody:
                        var questionBody = _answerFactory.ToEntity(answer);
                        entity.SetQuestionBody(questionBody);
                        break;
                    case AnswerKind.Comment:
                        var newComment = _commentFactory.ToEntity(answer);
                        entity.AddComment(newComment);
                        break;
                }
            });
        }
    }
}