using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DigitalHub.Application.Common;

public sealed class ApiResponse : IActionResult
{
    public ResultType Code { get; private set; }
    public string Message { get; private set; }
    public object Data { get; private set; }

    private ApiResponse(ResultType code, string message, object data)
        => (Code, Message, Data) = (code, message, data);

    public async Task ExecuteResultAsync(ActionContext context)
    {
        var objectResult = new ObjectResult(ApiResponseContract.Create(Code, Message, Data))
        {
            StatusCode = (int)HttpStatusCode.OK,
        };

        await objectResult.ExecuteResultAsync(context);
    }


    // Success Response
    public static ApiResponse Success(string message = "Success", object data = null!)
        => new ApiResponse(ResultType.Success, message, data);

    // Failure Response
    public static ApiResponse Failure(string message = "Failure", object data = null!)
        => new ApiResponse(ResultType.Failure, message, data);

    // Warning Response
    public static ApiResponse Warning(string message = "Warning", object data = null!)
        => new ApiResponse(ResultType.Warning, message, data);

    // Information Response
    public static ApiResponse Information(string message = "Information", object data = null!)
        => new ApiResponse(ResultType.Information, message, data);

    // Validation Errors Response
    public static ApiResponse ValidationErrors(string message = "Validation errors occurred", object data = null!)
        => new ApiResponse(ResultType.ValidationErrors, message, data);

    // Exception Response
    public static ApiResponse Exception(string message = "An exception occurred", object data = null!)
        => new ApiResponse(ResultType.Exception, message, data);

    // Rate Limited Response
    public static ApiResponse RateLimited(string message = "Too many requests", object data = null!)
        => new ApiResponse(ResultType.RateLimited, message, data);

    // Unauthorized Response
    public static ApiResponse Unauthorized(string message = "Unauthorized access", object data = null!)
        => new ApiResponse(ResultType.UnAuthorized, message, data);

    // Access Denied Response
    public static ApiResponse AccessDenied(string message = "Access denied", object data = null!)
        => new ApiResponse(ResultType.AccessDenied, message, data);
}
