using System;
using StackOverHead.LibCommon.Entities;

namespace StackOverHead.Question.Domain.Entities
{
    public class CommentEntity : Entity
    {
        public Guid UserId { get; private set; }
        public string Body { get; private set; }
        public Entity Parent { get; private set; }
        public CommentEntity(Guid userId, string body)
        {
            UserId = userId;
            Body = body;
        }

        public void SetParent(Entity parent)
        {
            Parent = parent;
        }

        public override bool IsValid()
        {
            return UserId != Guid.Empty && string.IsNullOrEmpty(Body);
        }
    }
}