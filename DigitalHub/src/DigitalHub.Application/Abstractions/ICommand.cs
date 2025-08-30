using MediatR;

namespace DigitalHub.Application.Abstractions;

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}
