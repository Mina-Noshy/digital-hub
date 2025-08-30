using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.HR.EmployeeMaster;
using DigitalHub.Domain.Interfaces.HR;
using DigitalHub.Domain.QueryParams.HR;

namespace DigitalHub.Application.Services.Master.EmployeeMaster;

public record GetEmployeeMastersQuery
    (EmployeeMasterQueryParams queryParams, CancellationToken cancellationToken = default) : IQuery<ApiResponse>;

public class GetEmployeeMastersQueryHandler(IEmployeeMasterRepository _repository) : IQueryHandler<GetEmployeeMastersQuery, ApiResponse>
{
    public async Task<ApiResponse> Handle(GetEmployeeMastersQuery request, CancellationToken cancellationToken)
    {
        var totalCount = await _repository.CountAsync(request.queryParams, cancellationToken);

        var entityList = await _repository.GetPagedAsync(request.queryParams, cancellationToken);

        var entityListDto = entityList.Select(x => new EmployeeMasterDto()
        {
            Id = x.Id,
            DepartmentId = x.DepartmentId,
            RoleId = x.RoleId,
            EmploymentTypeId = x.EmploymentTypeId,
            EmployeeCode = x.EmployeeCode,
            HireDate = x.HireDate,
            Salary = x.Salary,
            Currency = x.Currency,

            Department = x.GetDepartment.Name,
            Role = x.GetEmployeeRole?.Name,
            EmploymentType = x.GetEmploymentType?.Name,

            Title = x.Title,
            FirstName = x.FirstName,
            LastName = x.LastName,
            DateOfBirth = x.DateOfBirth,
            Gender = x.Gender,
            MaritalStatus = x.MaritalStatus,

            PhoneNumber = x.PhoneNumber,
            Email = x.Email,
            Country = x.Country,
            City = x.City,
            Address = x.Address,
            PostalCode = x.PostalCode,

            NationalID = x.NationalID,
            PassportNumber = x.PassportNumber,
            DriverLicenseNumber = x.DriverLicenseNumber,

            JobTitle = x.JobTitle,
            Employer = x.Employer,
            WorkPhoneNumber = x.WorkPhoneNumber,
            WorkEmail = x.WorkEmail,

            LinkedIn = x.LinkedIn,
            Facebook = x.Facebook,
            Twitter = x.Twitter,
            Instagram = x.Instagram,

            EmergencyContactName = x.EmergencyContactName,
            EmergencyContactPhoneNumber = x.EmergencyContactPhoneNumber,
            EmergencyContactRelationship = x.EmergencyContactRelationship,

            Notes = x.Notes
        });

        var pagedResponse = new PagedResponse<EmployeeMasterDto>(entityListDto, request.queryParams.PageNumber, request.queryParams.PageSize, totalCount);

        return ApiResponse.Success(data: pagedResponse);
    }
}
