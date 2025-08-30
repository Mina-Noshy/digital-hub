using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Master.UnitViewMaster;
using DigitalHub.Domain.Interfaces.Master;
using FluentValidation;
using Mapster;
using UnitViewMasterEntity = DigitalHub.Domain.Entities.Master.UnitViewMaster;

namespace DigitalHub.Application.Services.Master.UnitViewMaster;

public record CreateUnitViewMasterCommand
    (CreateUnitViewMasterDto requestDto, CancellationToken cancellationToken = default) : ICommand<ApiResponse>;


public class CreateUnitViewMasterCommandHandler(IUnitViewMasterRepository _repository) : ICommandHandler<CreateUnitViewMasterCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(CreateUnitViewMasterCommand request, CancellationToken cancellationToken = default)
    {
        var entity = request.requestDto.Adapt<UnitViewMasterEntity>();

        var result = await _repository.AddAsync(entity, cancellationToken);
        if (result)
        {
            var entityDto = entity.Adapt<UnitViewMasterDto>();
            return ApiResponse.Success(ApiMessage.SuccessfulCreate, entityDto);
        }
        return ApiResponse.Failure(ApiMessage.FailedCreate);
    }
}



public sealed class CreateUnitViewMasterCommandValidator : AbstractValidator<CreateUnitViewMasterCommand>
{
    public CreateUnitViewMasterCommandValidator()
    {
        RuleFor(x => x.requestDto.Name)
             .NotEmpty()
             .MinimumLength(2)
             .MaximumLength(50);
    }
}
