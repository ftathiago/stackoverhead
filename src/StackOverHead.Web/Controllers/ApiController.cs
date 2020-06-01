using Microsoft.AspNetCore.Mvc;
using StackOverHead.Web.Models;

namespace StackOverHead.Web.Controllers
{
    public class ApiController : Controller
    {
        protected IActionResult GetResponse<T>(T obj)
        {
            var response = new ResponseDefault<T>();
            response.Data = obj;
            return Ok(response);
        }
    }
}