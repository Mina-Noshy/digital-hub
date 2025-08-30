using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Auth.PageMaster;
using DigitalHub.Domain.Interfaces.Auth;
using FluentValidation;
using Mapster;
using PageMasterEntity = DigitalHub.Domain.Entities.Auth.PageMaster;

namespace DigitalHub.Application.Services.Auth.PageMaster;

public record CreatePageMasterCommand
    (CreatePageMasterDto requestDto, CancellationToken cancellationToken = default) : ICommand<ApiResponse>;


public class CreatePageMasterCommandHandler(IPageMasterRepository _repository) : ICommandHandler<CreatePageMasterCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(CreatePageMasterCommand request, CancellationToken cancellationToken = default)
    {
        var entity = request.requestDto.Adapt<PageMasterEntity>();

        var result = await _repository.AddAsync(entity, cancellationToken);
        if (result)
        {
            var entityDto = entity.Adapt<PageMasterDto>();
            return ApiResponse.Success(ApiMessage.SuccessfulCreate, entityDto);
        }
        return ApiResponse.Failure(ApiMessage.FailedCreate);
    }
}



public sealed class CreatePageMasterCommandValidator : AbstractValidator<CreatePageMasterCommand>
{
    public CreatePageMasterCommandValidator()
    {
        RuleFor(x => x.requestDto.Name)
             .NotEmpty()
             .MinimumLength(2)
             .MaximumLength(50);
    }
}
