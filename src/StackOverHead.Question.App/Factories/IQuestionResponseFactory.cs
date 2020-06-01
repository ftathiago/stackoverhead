using StackOverHead.LibCommon.Repositories;
using StackOverHead.Question.App.Models;
using StackOverHead.Question.Domain.Entities;

namespace StackOverHead.Question.App.Factories
{
    public interface IQuestionResponseFactory : IEntityDTOConverter<QuestionEntity, QuestionResponse>
    { }
}