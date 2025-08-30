using DigitalHub.Api.ExceptionHandlers;
using DigitalHub.Application.Services.Auth.Auth;
using FluentValidation;

namespace DigitalHub.Api.ServiceInjection;

public static class ExceptionHandlerExtension
{
    public static IServiceCollection AddExceptionHandlerExtension(this IServiceCollection services)
    {
        services.AddProblemDetails();
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddValidatorsFromAssemblyContaining<GetTokenQueryHandler>(); // Register MediatR commands and queries

        return services;
    }
}
