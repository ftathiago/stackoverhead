// <copyright file="ApiController.cs" company="BlogDoFT">
// Copyright (c) BlogDoFT. All rights reserved.
// </copyright>
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using StackOverHead.Web.Models;

namespace StackOverHead.Web.Controllers
{
    /// <summary>
    /// A Base class for ApiControllers.
    /// </summary>
    public class ApiController : Controller
    {
        /// <summary>
        /// Format API responses.
        /// </summary>
        /// <param name="obj">The object that will be returned to caller.</param>
        /// <param name="statusCodes">HTTP status code.</param>
        /// <typeparam name="T">Object's sent to caller.</typeparam>
        /// <returns>A object in JSON API format.</returns>
        protected IActionResult GetResponse<T>(T obj, int statusCodes = StatusCodes.Status200OK)
        {
            var response = new ResponseDefault<T>
            {
                Data = obj,
            };

            var result = new ObjectResult(response)
            {
                StatusCode = statusCodes,
            };

            if (obj == null)
            {
                result.StatusCode = StatusCodes.Status204NoContent;
            }

            return result;
        }

        /// <summary>
        /// Validate and return model errors.
        /// </summary>
        /// <returns>A ErrorList with statusCode 500.</returns>
        protected IActionResult GetModelErrorResponse()
        {
            if (ModelState.IsValid)
            {
                return Ok();
            }

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
            var response = new ResponseDefault<object>
            {
                Data = null,
                Message = string.Join("\n", errors),
            };
            return new ObjectResult(response)
            {
                StatusCode = StatusCodes.Status500InternalServerError,
            };
        }
    }
}