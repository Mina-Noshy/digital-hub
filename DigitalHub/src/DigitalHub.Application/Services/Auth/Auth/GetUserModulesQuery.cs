using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Auth.ModuleMaster;
using DigitalHub.Domain.Interfaces.Auth;
using DigitalHub.Domain.Interfaces.Common;

namespace DigitalHub.Application.Services.Auth.Auth;

public record GetUserModulesQuery(CancellationToken cancellationToken = default) : IQuery<ApiResponse>;


public class GetUserModulesQueryHandler(IModuleMasterRepository _moduleMasterRepository, ICurrentUser _currentUser) : IQueryHandler<GetUserModulesQuery, ApiResponse>
{
    public async Task<ApiResponse> Handle(GetUserModulesQuery request, CancellationToken cancellationToken)
    {
        // Try to get current user id
        var userId = _currentUser.UserId;

        var entityList = await _moduleMasterRepository.GetUserModules(userId, cancellationToken);

        var entityListDto = entityList.Select(x => new ModuleMasterDto()
        {
            Id = x.Id,
            BackColor = x.BackColor,
            Background = x.Background,
            FrontColor = x.FrontColor,
            Icon = x.Icon,
            Label = x.Label,
            Name = x.Name,
            Path = x.Path,
            Description = x.Description,
            I18nKey = x.I18nKey,
            SortOrder = x.SortOrder,
        })
        .Distinct()
        .OrderBy(x => x.SortOrder);


        return ApiResponse.Success(data: entityListDto);
    }


}