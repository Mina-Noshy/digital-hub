using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Master.UnitTypeMaster;
using DigitalHub.Domain.Interfaces.Master;
using FluentValidation;
using Mapster;
using UnitTypeMasterEntity = DigitalHub.Domain.Entities.Master.UnitTypeMaster;

namespace DigitalHub.Application.Services.Master.UnitTypeMaster;

public record CreateUnitTypeMasterCommand
    (CreateUnitTypeMasterDto requestDto, CancellationToken cancellationToken = default) : ICommand<ApiResponse>;


public class CreateUnitTypeMasterCommandHandler(IUnitTypeMasterRepository _repository) : ICommandHandler<CreateUnitTypeMasterCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(CreateUnitTypeMasterCommand request, CancellationToken cancellationToken = default)
    {
        var entity = request.requestDto.Adapt<UnitTypeMasterEntity>();

        var result = await _repository.AddAsync(entity, cancellationToken);
        if (result)
        {
            var entityDto = entity.Adapt<UnitTypeMasterDto>();
            return ApiResponse.Success(ApiMessage.SuccessfulCreate, entityDto);
        }
        return ApiResponse.Failure(ApiMessage.FailedCreate);
    }
}



public sealed class CreateUnitTypeMasterCommandValidator : AbstractValidator<CreateUnitTypeMasterCommand>
{
    public CreateUnitTypeMasterCommandValidator()
    {
        RuleFor(x => x.requestDto.Name)
             .NotEmpty()
             .MinimumLength(2)
             .MaximumLength(50);
    }
}
