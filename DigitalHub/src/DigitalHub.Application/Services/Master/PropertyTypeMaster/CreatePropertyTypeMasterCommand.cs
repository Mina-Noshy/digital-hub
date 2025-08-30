using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Master.PropertyTypeMaster;
using DigitalHub.Domain.Interfaces.Master;
using FluentValidation;
using Mapster;
using PropertyTypeMasterEntity = DigitalHub.Domain.Entities.Master.PropertyTypeMaster;

namespace DigitalHub.Application.Services.Master.PropertyTypeMaster;

public record CreatePropertyTypeMasterCommand
    (CreatePropertyTypeMasterDto requestDto, CancellationToken cancellationToken = default) : ICommand<ApiResponse>;


public class CreatePropertyTypeMasterCommandHandler(IPropertyTypeMasterRepository _repository) : ICommandHandler<CreatePropertyTypeMasterCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(CreatePropertyTypeMasterCommand request, CancellationToken cancellationToken = default)
    {
        var entity = request.requestDto.Adapt<PropertyTypeMasterEntity>();

        var result = await _repository.AddAsync(entity, cancellationToken);
        if (result)
        {
            var entityDto = entity.Adapt<PropertyTypeMasterDto>();
            return ApiResponse.Success(ApiMessage.SuccessfulCreate, entityDto);
        }
        return ApiResponse.Failure(ApiMessage.FailedCreate);
    }
}



public sealed class CreatePropertyTypeMasterCommandValidator : AbstractValidator<CreatePropertyTypeMasterCommand>
{
    public CreatePropertyTypeMasterCommandValidator()
    {
        RuleFor(x => x.requestDto.Name)
             .NotEmpty()
             .MinimumLength(2)
             .MaximumLength(50);
    }
}
