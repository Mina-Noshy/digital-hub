using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Master.UnitModelMaster;
using DigitalHub.Domain.Interfaces.Master;
using FluentValidation;
using Mapster;
using UnitModelMasterEntity = DigitalHub.Domain.Entities.Master.UnitModelMaster;

namespace DigitalHub.Application.Services.Master.UnitModelMaster;

public record CreateUnitModelMasterCommand
    (CreateUnitModelMasterDto requestDto, CancellationToken cancellationToken = default) : ICommand<ApiResponse>;


public class CreateUnitModelMasterCommandHandler(IUnitModelMasterRepository _repository) : ICommandHandler<CreateUnitModelMasterCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(CreateUnitModelMasterCommand request, CancellationToken cancellationToken = default)
    {
        var entity = request.requestDto.Adapt<UnitModelMasterEntity>();

        var result = await _repository.AddAsync(entity, cancellationToken);
        if (result)
        {
            var entityDto = entity.Adapt<UnitModelMasterDto>();
            return ApiResponse.Success(ApiMessage.SuccessfulCreate, entityDto);
        }
        return ApiResponse.Failure(ApiMessage.FailedCreate);
    }
}



public sealed class CreateUnitModelMasterCommandValidator : AbstractValidator<CreateUnitModelMasterCommand>
{
    public CreateUnitModelMasterCommandValidator()
    {
        RuleFor(x => x.requestDto.Name)
             .NotEmpty()
             .MinimumLength(2)
             .MaximumLength(50);
    }
}
