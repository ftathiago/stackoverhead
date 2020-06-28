using System;
using System.Collections.Generic;

using StackOverHead.LibCommon.Entities;
using StackOverHead.Question.Domain.Enums;

namespace StackOverHead.Question.Domain.Entities
{
    public class AnswerEntity : Entity
    {
        public string Body { get; }
        public Guid UserId { get; }
        public int Votes { get; }
        public AnswerKind Kind { get; }
        public List<CommentEntity> Comments { get; }
        public Entity Parent { get; private set; }

        public AnswerEntity(string body, Guid userId, AnswerKind kind, int votes)
        {
            Body = body;
            UserId = userId;
            Votes = votes;
            Kind = kind;
            Comments = new List<CommentEntity>();
        }

        public void SetParent(Entity entity)
        {
            Parent = entity;
        }

        public void AddComment(CommentEntity comment)
        {
            Comments.Add(comment);
        }

        public override bool IsValid()
        {
            throw new System.NotImplementedException();
        }
    }
}