using System;
using System.Collections.Generic;
using StackOverHead.LibCommon.Entities;
using StackOverHead.Question.Domain.Enums;

namespace StackOverHead.Question.Domain.Entities
{
    public class QuestionEntity : Entity
    {
        public string Title { get; private set; }
        public Guid UserId { get; private set; }
        public string Tags { get; private set; }
        public IEnumerable<CommentEntity> Comments { get => _comments; }
        private readonly List<CommentEntity> _comments;
        public IEnumerable<AnswerEntity> Answers { get => _answers; }
        private readonly List<AnswerEntity> _answers;
        public AnswerEntity QuestionBody { get; private set; }

        public QuestionEntity(string title, Guid userId, string tags)
        {
            Title = title;
            UserId = userId;
            Tags = tags;
            _answers = new List<AnswerEntity>();
            _comments = new List<CommentEntity>();
        }

        public void AddComment(CommentEntity comment)
        {
            comment.SetParent(this);
            _comments.Add(comment);
        }

        public void AddAnswer(AnswerEntity answer)
        {
            if (answer.Kind != AnswerKind.Answer)
                throw new ArgumentException("You only can add Answers!");

            answer.SetParent(this);
            _answers.Add(answer);
        }

        public void SetQuestionBody(AnswerEntity questionBody)
        {
            if (questionBody.Kind != AnswerKind.QuestionBody)
                throw new ArgumentException($"You only can set a \"Question body\" type answer");
            questionBody.SetParent(this);
            QuestionBody = questionBody;
        }

        public override bool IsValid()
        {
            throw new System.NotImplementedException();
        }
    }
}