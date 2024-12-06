using Microsoft.AspNetCore.Mvc;
using Shared;
using System.Net;

namespace E_Commerce_Original.Factories
{
    public class ApiResponseFactory
    {
        public static IActionResult CustomValidationErrorResponse(ActionContext actionContext)
        {
            var error = actionContext.ModelState.Where(error => error.Value.Errors.Any())
                .Select(error => new ValidationError
                {
                    Field = error.Key,
                    Errors = error.Value.Errors.Select(error=>error.ErrorMessage)
                });

            var response = new ValidationErrorResponse
            {
                StatusCode = (int) HttpStatusCode.BadRequest,
                ErrorMessage = "Validation Failed",
                Errors = error
            };

            return new BadRequestObjectResult(response);
        }
    }
}
