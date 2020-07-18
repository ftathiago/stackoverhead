using Bogus;
using System;

namespace StackOverHead.Question.Elastic.Tests.Fixtures
{
    public class BaseFixture
    {
        protected Lazy<Faker> _faker;

        public BaseFixture()
        {
            _faker = new Lazy<Faker>(() => new Faker("pt_BR"));
        }

        public Faker Faker() =>
            _faker.Value;
    }
}