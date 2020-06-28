using Moq;

using StackOverHead.Question.App.Services;
using StackOverHead.Web.Controllers;

namespace StackOverHead.Web.Tests.Fixtures
{
    public class QuestionControllerFixtures
    {
        public QuestionController QuestionControllerFactory(Mock<IQuestionService> questionService = null)
        {
            var service = questionService ?? MockQuestionService();

            return new QuestionController(service.Object, null);
        }

        public Mock<IQuestionService> MockQuestionService()
        {
            return new Mock<IQuestionService>(MockBehavior.Strict);
        }
    }
}