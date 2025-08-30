using DigitalHub.Application.Common;
using DigitalHub.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Serilog;
using System.Net;

namespace DigitalHub.Api.ExceptionHandlers;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        httpContext.Response.ContentType = "application/json";

        var excDetails = exception switch
        {
            ValidationAppException => (
                Detail: exception.Message,
                StatusCode: StatusCodes.Status200OK),
            _ => (
                Detail: exception.Message,
                StatusCode: (int)HttpStatusCode.OK)
        };

        // Return validation message
        httpContext.Response.StatusCode = excDetails.StatusCode;
        if (exception is ValidationAppException validationAppException)
        {
            var errorMessages = string.Join(" | ", validationAppException.Errors
                .SelectMany(e => e.Value));

            var validationExceptionResponse =
                ApiResponse.ValidationErrors(errorMessages);

            await httpContext.Response.WriteAsJsonAsync(validationExceptionResponse);
            return true;
        }

        // Log the exception
        Log.Error(exception, excDetails.Detail);


        // Return exception message
        var response = ApiResponse.Exception(exception.Message);
        await httpContext.Response.WriteAsJsonAsync(response);
        return true;
    }

}
