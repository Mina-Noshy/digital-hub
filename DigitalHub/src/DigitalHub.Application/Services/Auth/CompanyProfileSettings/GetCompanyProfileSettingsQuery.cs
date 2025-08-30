using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Auth.CompanyProfileSettings;
using DigitalHub.Domain.Interfaces.Auth;
using DigitalHub.Domain.Interfaces.Common;
using Mapster;

namespace DigitalHub.Application.Services.Auth.CompanyProfileSettings;


public record GetCompanyProfileSettingsQuery
    (CancellationToken cancellationToken = default) : IQuery<ApiResponse>;

public class GetCompanyProfileSettingsQueryHandler(ICompanyProfileSettingsRepository _repository, IPathFinderService _pathFinderService) : IQueryHandler<GetCompanyProfileSettingsQuery, ApiResponse>
{
    public async Task<ApiResponse> Handle(GetCompanyProfileSettingsQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetSettingsAsync(cancellationToken);

        if (entity == null)
        {
            return ApiResponse.Failure(ApiMessage.ItemNotFound);
        }

        var entityDto = entity.Adapt<CompanyProfileSettingsDto>();
        if (!string.IsNullOrWhiteSpace(entity.Logo))
        {
            var folder = _pathFinderService.CompanyLogoFolderPath;
            entityDto.LogoUrl = _pathFinderService.GetFullURL(folder, entity.Logo);
        }

        return ApiResponse.Success(data: entityDto);
    }
}