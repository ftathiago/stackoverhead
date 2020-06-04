using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using StackOverHead.Web.Models;

namespace StackOverHead.Web.Controllers
{
    public class ApiController : Controller
    {
        protected IActionResult GetResponse<T>(T obj, int statusCodes = StatusCodes.Status200OK)
        {
            var response = new ResponseDefault<T>();
            response.Data = obj;

            var result = new ObjectResult(response);
            result.StatusCode = statusCodes;
            if (obj == null)
                result.StatusCode = StatusCodes.Status204NoContent;

            return result;
        }

        protected IActionResult GetModelErrorResponse()
        {
            if (ModelState.IsValid) return Ok();

            var errors = new List<string>();
            ModelState.ToList().ForEach(error =>
            {
                error.Value.Errors.ToList()
                    .ForEach(message => errors.Add(message.ErrorMessage));
            });

            return ErrorResponse(errors);
        }

        private IActionResult ErrorResponse(List<string> errors)
        {
            var response = new ResponseDefault<object>();
            response.Data = null;
            response.message = string.Join("\n", errors);
            var result = new ObjectResult(response);
            result.StatusCode = StatusCodes.Status500InternalServerError;
            return result;
        }
    }
}