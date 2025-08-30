using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Auth.ModuleMaster;
using DigitalHub.Domain.Interfaces.Auth;
using FluentValidation;
using Mapster;
using ModuleMasterEntity = DigitalHub.Domain.Entities.Auth.ModuleMaster;

namespace DigitalHub.Application.Services.Auth.ModuleMaster;

public record CreateModuleMasterCommand
    (CreateModuleMasterDto requestDto, CancellationToken cancellationToken = default) : ICommand<ApiResponse>;


public class CreateModuleMasterCommandHandler(IModuleMasterRepository _repository) : ICommandHandler<CreateModuleMasterCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(CreateModuleMasterCommand request, CancellationToken cancellationToken = default)
    {
        var entity = request.requestDto.Adapt<ModuleMasterEntity>();

        var result = await _repository.AddAsync(entity, cancellationToken);
        if (result)
        {
            var entityDto = entity.Adapt<ModuleMasterDto>();
            return ApiResponse.Success(ApiMessage.SuccessfulCreate, entityDto);
        }
        return ApiResponse.Failure(ApiMessage.FailedCreate);
    }
}



public sealed class CreateModuleMasterCommandValidator : AbstractValidator<CreateModuleMasterCommand>
{
    public CreateModuleMasterCommandValidator()
    {
        RuleFor(x => x.requestDto.Name)
             .NotEmpty()
             .MinimumLength(2)
             .MaximumLength(50);
    }
}
