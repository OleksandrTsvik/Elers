using Application.Common.Interfaces;
using Domain.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Api.Middleware;

public static class InvalidModelStateResponseBuilder
{
    public static IActionResult BuildResponse(ActionContext context)
    {
        IHostEnvironment environment = context.HttpContext.RequestServices
            .GetRequiredService<IHostEnvironment>();

        ITranslator translator = context.HttpContext.RequestServices
            .GetRequiredService<ITranslator>();

        int statusCode = StatusCodes.Status400BadRequest;
        string message = translator.GetString("Bad Request. Please check your data and try again.");
        object? details = null;

        if (environment.IsDevelopment())
        {
            details = GetModelStateErrors(context.ModelState);
        }

        var exceptionResponse = new ExceptionResponse(statusCode, message, details);

        return new ObjectResult(exceptionResponse)
        {
            StatusCode = exceptionResponse.StatusCode
        };
    }

    private static List<ValidationError> GetModelStateErrors(ModelStateDictionary modelState)
    {
        var errors = new List<ValidationError>();

        foreach (string key in modelState.Keys)
        {
            ModelStateEntry? modelStateValue = modelState[key];

            if (modelStateValue is null)
            {
                continue;
            }

            var validationErrors = modelStateValue.Errors
                .Select(x => new ValidationError(key, x.ErrorMessage))
                .ToList();

            errors.AddRange(validationErrors);
        }

        return errors;
    }
}
