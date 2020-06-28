using FluentValidation.Results;

namespace StackOverHead.LibCommon.ValueObjects
{
    public abstract class ValueObject<T>
        where T : ValueObject<T>
    {
        public ValidationResult ValidationResult { get => _validationResult; }
        protected ValidationResult _validationResult = new ValidationResult();

        public override bool Equals(object obj)
        {
            return obj is T valueObject && EqualsCore(valueObject);
        }

        protected abstract bool EqualsCore(T other);

        public override int GetHashCode()
        {
            return GetHashCodeCore();
        }

        protected abstract int GetHashCodeCore();

        public abstract bool IsValid();
    }
}
