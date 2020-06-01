using System;
using System.Threading.Tasks;
using StackOverHead.Question.App.Models;

namespace StackOverHead.Question.App.Services
{
    public interface IQuestionService
    {
        Task<Guid> Add(AskQuestion question);

        Task<QuestionResponse> GetById(Guid id);
    }
}