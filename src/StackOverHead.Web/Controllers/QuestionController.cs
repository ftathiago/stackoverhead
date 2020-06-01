using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StackOverHead.Auth.App.Services;
using StackOverHead.Question.App.Models;
using StackOverHead.Question.App.Services;
using StackOverHead.Web.Models;

namespace StackOverHead.Web.Controllers
{
    [Route("api/[Controller]")]
    public class QuestionController : ApiController
    {
        private readonly IQuestionService _question;
        private readonly ILogger<QuestionController> _logger;
        private readonly IUserService _user;

        public QuestionController(IQuestionService question,
            ILogger<QuestionController> logger,
            IUserService user)
        {
            _question = question;
            _logger = logger;
            _user = user;
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ResponseDefault<Guid>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseDefault<Guid>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] AskQuestion question)
        {
            var id = await _question.Add(question);

            return GetResponse<Guid>(id);// Created("/api/question", new { id });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuestionById([FromRoute] Guid id)
        {
            var response = await _question.GetById(id);
            if (response.User != null)
            {
                var user = _user.GetUserById(response.User.Id);
                if (user != null)
                    response.User.Name = user.FullName;
            }
            return GetResponse<QuestionResponse>(response);
        }
    }
}