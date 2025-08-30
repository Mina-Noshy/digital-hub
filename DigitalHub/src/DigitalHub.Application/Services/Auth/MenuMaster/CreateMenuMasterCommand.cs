using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Auth.MenuMaster;
using DigitalHub.Domain.Interfaces.Auth;
using FluentValidation;
using Mapster;
using MenuMasterEntity = DigitalHub.Domain.Entities.Auth.MenuMaster;

namespace DigitalHub.Application.Services.Auth.MenuMaster;

public record CreateMenuMasterCommand
    (CreateMenuMasterDto requestDto, CancellationToken cancellationToken = default) : ICommand<ApiResponse>;


public class CreateMenuMasterCommandHandler(IMenuMasterRepository _repository) : ICommandHandler<CreateMenuMasterCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(CreateMenuMasterCommand request, CancellationToken cancellationToken = default)
    {
        var entity = request.requestDto.Adapt<MenuMasterEntity>();

        var result = await _repository.AddAsync(entity, cancellationToken);
        if (result)
        {
            var entityDto = entity.Adapt<MenuMasterDto>();
            return ApiResponse.Success(ApiMessage.SuccessfulCreate, entityDto);
        }
        return ApiResponse.Failure(ApiMessage.FailedCreate);
    }
}



public sealed class CreateMenuMasterCommandValidator : AbstractValidator<CreateMenuMasterCommand>
{
    public CreateMenuMasterCommandValidator()
    {
        RuleFor(x => x.requestDto.Name)
             .NotEmpty()
             .MinimumLength(2)
             .MaximumLength(50);
    }
}
