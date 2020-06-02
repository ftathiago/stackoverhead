using StackOverHead.LibCommon.Repositories;
using StackOverHead.Question.Domain.Entities;
using StackOverHead.Question.Infra.Models;

namespace StackOverHead.Question.Infra.Factories
{
    public interface IAnswerEntityModelFactory : IEntityDtoConverter<AnswerEntity, AnswerModel>
    { }
}