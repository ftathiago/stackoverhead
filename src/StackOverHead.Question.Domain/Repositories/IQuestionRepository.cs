using System.Threading.Tasks;
using StackOverHead.LibCommon.Repositories;
using StackOverHead.Question.Domain.Entities;

namespace StackOverHead.Question.Domain.Repositories
{
    public interface IQuestionRepository : IBaseRepository<QuestionEntity>
    {
        Task RegisterCommentAsync(CommentEntity comment);
    }

}