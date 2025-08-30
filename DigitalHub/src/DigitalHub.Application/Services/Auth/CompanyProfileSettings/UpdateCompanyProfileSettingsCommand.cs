using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Auth.CompanyProfileSettings;
using DigitalHub.Domain.Extensions;
using DigitalHub.Domain.Interfaces.Auth;
using DigitalHub.Domain.Interfaces.Common;
using FluentValidation;
using Mapster;

namespace DigitalHub.Application.Services.Auth.CompanyProfileSettings;


public record UpdateCompanyProfileSettingsCommand
    (UpdateCompanyProfileSettingsDto requestDto, CancellationToken cancellationToken = default) : ICommand<ApiResponse>;



public class UpdateCompanyProfileSettingsCommandHandler(ICompanyProfileSettingsRepository _repository, IPathFinderService _pathFinderService) : ICommandHandler<UpdateCompanyProfileSettingsCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(UpdateCompanyProfileSettingsCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetSettingsAsync(cancellationToken);
        if (entity == null)
        {
            return ApiResponse.Failure(ApiMessage.ItemNotFound);
        }

        entity.UpdateFromDto(request.requestDto);

        if (request.requestDto.Logo != null)
        {
            var fileName = request.requestDto.Logo.GenerateUniqueFileName();
            await request.requestDto.Logo.UploadToAsync(_pathFinderService.CompanyLogoFolderPath, fileName);
            entity.Logo = fileName;
        }

        var result = await _repository.UpdateAsync(entity, cancellationToken);

        if (result)
        {
            var entityDto = entity.Adapt<CompanyProfileSettingsDto>();
            return ApiResponse.Success(ApiMessage.SuccessfulUpdate, entityDto);
        }
        return ApiResponse.Failure(ApiMessage.FailedUpdate);
    }
}


public sealed class UpdateCompanyProfileSettingsCommandValidator : AbstractValidator<UpdateCompanyProfileSettingsCommand>
{
    public UpdateCompanyProfileSettingsCommandValidator()
    {
        RuleFor(x => x.requestDto.CompanyName)
             .NotEmpty()
             .MinimumLength(2)
             .MaximumLength(50);

        RuleFor(x => x.requestDto.CompanyDescription)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(2000);

        RuleFor(x => x.requestDto.SupportEmail)
             .NotEmpty()
             .EmailAddress();

    }
}