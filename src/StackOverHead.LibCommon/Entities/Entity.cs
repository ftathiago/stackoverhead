using System;

using FluentValidation.Results;

namespace StackOverHead.LibCommon.Entities
{
    public abstract class Entity
    {
        public ValidationResult ValidationResult { get => _validationResult; }
        protected ValidationResult _validationResult = new ValidationResult();

        public Guid Id { get; protected set; }

        public void DefineId(Guid id)
        {
            this.Id = id;
        }

        public abstract bool IsValid();

        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            var compareTo = obj as Entity;

            return Id.Equals(compareTo.Id);
        }

        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 42) + Id.GetHashCode();
        }
    }
}
