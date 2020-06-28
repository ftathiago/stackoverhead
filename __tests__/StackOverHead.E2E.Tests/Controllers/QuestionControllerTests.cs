using Xunit;
using StackOverHead.Question.App.Models;
using System;
using StackOverHead.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using StackOverHead.E2E.Tests.Fixtures;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;
using Microsoft.AspNetCore;
using StackOverHead.Web;
using Microsoft.AspNetCore.Hosting;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace StackOverHead.E2E.Tests.Controllers
{
    public class QuestionControllerTests :
        IClassFixture<StackOverHeadWebFixtures>,
        IDisposable
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public QuestionControllerTests(StackOverHeadWebFixtures web)
        {
            _server = web.GetWebServer();
            _client = _server.CreateClient();
        }

        public void Dispose()
        {
            _client.Dispose();
            _server.Dispose();
        }

        [Fact]
        public async Task ShouldRegisterANewQuestion()
        {
            var question = new AskQuestion
            {
                Title = "How to ask a question?",
                Body = "<strong>This is a body with HTML support</strong>",
                Tags = "OneTag,TwoTag",
                UserId = Guid.NewGuid()
            };
            var content = new StringContent(
                JsonConvert.SerializeObject(question), Encoding.UTF8, "application/json"
            );

            var response = await _client.PostAsync("api/question", content);

            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var questionAsked = JsonConvert.DeserializeObject<AskQuestion>(responseString);
        }
    }
}