using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackOverHead.Web.Enums;
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
            return result;
        }
    }
}