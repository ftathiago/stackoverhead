using Xunit;
using StackOverHead.Question.App.Models;
using System;
using StackOverHead.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using StackOverHead.Web.Tests.Fixtures;

namespace StackOverHead.Web.Tests.Controllers
{
    public class QuestionControllerTests : IClassFixture<QuestionControllerFixtures>
    {
        private readonly QuestionControllerFixtures _controllerFixtures;

        public QuestionControllerTests(QuestionControllerFixtures controllerFixtures)
        {
            _controllerFixtures = controllerFixtures;
        }

        [Fact]
        public void ShouldRegisterANewQuestion()
        {
            var question = new AskQuestion();
            question.Title = "How to ask a question?";
            question.Body = "<strong>This is a body with HTML support</strong>";
            question.Tags = "OneTag,TwoTag";
            question.UserId = Guid.NewGuid();
            var questionService = _controllerFixtures.MockQuestionService();
            questionService
                .Setup(s => s.Add(question));
            var controller = _controllerFixtures.QuestionControllerFactory(questionService);

            var response = controller.Post(question);

            // Assert.IsType<CreatedResult>(response);
            // var createdResult = (AskQuestion)response.As<CreatedResult>().Value;
            // createdResult.Should().BeEquivalentTo(question);
        }
    }
}