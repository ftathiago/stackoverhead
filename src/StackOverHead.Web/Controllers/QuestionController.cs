using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        private readonly IUserService _user;

        public QuestionController(
            IQuestionService question,
            IUserService user)
        {
            _question = question;
            _user = user;
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
            return GetResponse<QuestionResponse>(response, StatusCodes.Status200OK);
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ResponseDefault<Guid>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseDefault<Guid>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] AskQuestion question)
        {
            var id = await _question.Add(question);

            return GetResponse<Guid>(id, StatusCodes.Status201Created);
        }

        [HttpPost("{questionId}/comment/")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ResponseDefault<Guid>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseDefault<Guid>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddQuestionComment(Guid questionId, [FromBody] QuestionCommentRequest request)
        {
            if (!ModelState.IsValid)
            {
                return GetModelErrorResponse();
            }
            request.QuestionId = questionId;

            var commentId = await _question.RegisterQuestionComment(request);

            return GetResponse<Guid>(commentId, StatusCodes.Status201Created);
        }

        [HttpPost("{questionId}/answers")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ResponseDefault<Guid>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseDefault<Guid>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddAnswer(Guid questionId, [FromBody] AnswerRequest request)
        {
            if (!ModelState.IsValid)
            {
                return GetModelErrorResponse();
            }
            request.QuestionId = questionId;
            // request.UserId = extract from token payload

            var answerId = await _question.RegisterAnswer(request);

            return GetResponse<Guid>(answerId, StatusCodes.Status201Created);
        }

        [HttpPost("{questionId}/answers/{answerId}/comment")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ResponseDefault<Guid>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseDefault<Guid>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddAnswerComment(Guid questionId, Guid answerId, [FromBody] AnswerCommentRequest request)
        {
            if (!ModelState.IsValid)
            {
                return GetModelErrorResponse();
            }
            request.QuestionId = questionId;
            request.AnswerId = answerId;
            // request.UserId = extract from token payload

            var commentId = await _question.RegisterAnswerComment(request);

            return GetResponse<Guid>(commentId, StatusCodes.Status201Created);
        }
    }
}