using MediatR;

namespace DigitalHub.Application.Abstractions;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}
