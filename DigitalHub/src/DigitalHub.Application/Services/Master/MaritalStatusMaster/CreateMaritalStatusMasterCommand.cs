using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Master.MaritalStatusMaster;
using DigitalHub.Domain.Interfaces.Master;
using FluentValidation;
using Mapster;
using MaritalStatusMasterEntity = DigitalHub.Domain.Entities.Master.MaritalStatusMaster;

namespace DigitalHub.Application.Services.Master.MaritalStatusMaster;

public record CreateMaritalStatusMasterCommand
    (CreateMaritalStatusMasterDto requestDto, CancellationToken cancellationToken = default) : ICommand<ApiResponse>;


public class CreateMaritalStatusMasterCommandHandler(IMaritalStatusMasterRepository _repository) : ICommandHandler<CreateMaritalStatusMasterCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(CreateMaritalStatusMasterCommand request, CancellationToken cancellationToken = default)
    {
        var entity = request.requestDto.Adapt<MaritalStatusMasterEntity>();

        var result = await _repository.AddAsync(entity, cancellationToken);
        if (result)
        {
            var entityDto = entity.Adapt<MaritalStatusMasterDto>();
            return ApiResponse.Success(ApiMessage.SuccessfulCreate, entityDto);
        }
        return ApiResponse.Failure(ApiMessage.FailedCreate);
    }
}



public sealed class CreateMaritalStatusMasterCommandValidator : AbstractValidator<CreateMaritalStatusMasterCommand>
{
    public CreateMaritalStatusMasterCommandValidator()
    {
        RuleFor(x => x.requestDto.Name)
             .NotEmpty()
             .MinimumLength(2)
             .MaximumLength(50);
    }
}
