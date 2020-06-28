using System;
using System.Threading.Tasks;

using StackOverHead.Question.Domain.Entities;

namespace StackOverHead.Question.Domain.Lib
{
    public interface IQuestionEventLauncher
    {
        Task Publish(QuestionEntity askQuestion);
        Task Publish(Guid questionId, Guid answerId, CommentEntity comment);
        Task Publish(AnswerEntity answer);
    }
}