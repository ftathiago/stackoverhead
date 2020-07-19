using StackOverHead.Question.Elastic.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StackOverHead.Question.Elastic.Repositories
{
    public interface IAnswerRepository
    {
        Task AddAsync(Answer answer);
        Task UpdateAsync(Answer answer);
        Task RemoveAsync(Answer answer);

        Task<IEnumerable<Answer>> SearchAsync(
            string content,
            string tags,
            int page,
            int pageSize);
    }
}