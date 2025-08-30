using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Master.BankMaster;
using DigitalHub.Domain.Interfaces.Master;
using FluentValidation;
using Mapster;

namespace DigitalHub.Application.Services.Master.BankMaster;

public record UpdateBankMasterCommand
    (long id, UpdateBankMasterDto requestDto, CancellationToken cancellationToken = default) : ICommand<ApiResponse>;



public class UpdateBankMasterCommandHandler(IBankMasterRepository _repository) : ICommandHandler<UpdateBankMasterCommand, ApiResponse>
{

    public async Task<ApiResponse> Handle(UpdateBankMasterCommand request, CancellationToken cancellationToken)
    {
        if (request.id != request.requestDto.Id)
        {
            return ApiResponse.Failure(ApiMessage.EntityIdMismatch);
        }

        var entity = await _repository.GetByIdAsync(request.id, cancellationToken);
        if (entity == null)
        {
            return ApiResponse.Failure(ApiMessage.ItemNotFound);
        }

        entity.UpdateFromDto(request.requestDto);
        var result = await _repository.UpdateAsync(entity, cancellationToken);

        if (result)
        {
            var entityDto = entity.Adapt<BankMasterDto>();
            return ApiResponse.Success(ApiMessage.SuccessfulUpdate, entityDto);
        }
        return ApiResponse.Failure(ApiMessage.FailedUpdate);
    }
}


public sealed class UpdateBankMasterCommandValidator : AbstractValidator<UpdateBankMasterCommand>
{
    public UpdateBankMasterCommandValidator()
    {
        RuleFor(x => x.requestDto.BankName)
             .NotEmpty()
             .MinimumLength(2)
             .MaximumLength(50);
    }
}