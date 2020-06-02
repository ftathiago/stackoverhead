using System;
using System.Collections.Generic;
using StackOverHead.LibCommon.Entities;
using StackOverHead.Question.Domain.Enums;

namespace StackOverHead.Question.Domain.Entities
{
    public class AnswerEntity : Entity
    {
        public string Body { get; private set; }
        public Guid UserId { get; private set; }
        public int Votes { get; private set; }
        public AnswerKind Kind { get; private set; }
        public List<CommentEntity> Comments { get => _comments; }
        public Entity Parent { get; private set; }
        private readonly List<CommentEntity> _comments;

        public AnswerEntity(string body, Guid userId, AnswerKind kind, int votes)
        {
            Body = body;
            UserId = userId;
            Votes = votes;
            Kind = kind;
            _comments = new List<CommentEntity>();
        }

        public void SetParent(Entity entity)
        {
            Parent = entity;
        }

        public void AddComment(CommentEntity comment)
        {
            _comments.Add(comment);
        }

        public override bool IsValid()
        {
            throw new System.NotImplementedException();
        }
    }
}