using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Master.UnitClassMaster;
using DigitalHub.Domain.Interfaces.Master;
using FluentValidation;
using Mapster;
using UnitClassMasterEntity = DigitalHub.Domain.Entities.Master.UnitClassMaster;

namespace DigitalHub.Application.Services.Master.UnitClassMaster;

public record CreateUnitClassMasterCommand
    (CreateUnitClassMasterDto requestDto, CancellationToken cancellationToken = default) : ICommand<ApiResponse>;


public class CreateUnitClassMasterCommandHandler(IUnitClassMasterRepository _repository) : ICommandHandler<CreateUnitClassMasterCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(CreateUnitClassMasterCommand request, CancellationToken cancellationToken = default)
    {
        var entity = request.requestDto.Adapt<UnitClassMasterEntity>();

        var result = await _repository.AddAsync(entity, cancellationToken);
        if (result)
        {
            var entityDto = entity.Adapt<UnitClassMasterDto>();
            return ApiResponse.Success(ApiMessage.SuccessfulCreate, entityDto);
        }
        return ApiResponse.Failure(ApiMessage.FailedCreate);
    }
}



public sealed class CreateUnitClassMasterCommandValidator : AbstractValidator<CreateUnitClassMasterCommand>
{
    public CreateUnitClassMasterCommandValidator()
    {
        RuleFor(x => x.requestDto.Name)
             .NotEmpty()
             .MinimumLength(2)
             .MaximumLength(50);
    }
}
