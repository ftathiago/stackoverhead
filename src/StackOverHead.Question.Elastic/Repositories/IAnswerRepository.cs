using System.Collections.Generic;
using System.Threading.Tasks;
using StackOverHead.Question.Elastic.Models;

namespace StackOverHead.Question.Elastic.Repositories
{
    public interface IAnswerRepository
    {
        Task AddAsync(Answer question);
        Task UpdateAsync(Answer question);
        Task RemoveAsync(Answer model);
        Task<IEnumerable<Answer>> SearchAsync(
            string content,
            string tags,
            int page,
            int pageSize);
    }
}