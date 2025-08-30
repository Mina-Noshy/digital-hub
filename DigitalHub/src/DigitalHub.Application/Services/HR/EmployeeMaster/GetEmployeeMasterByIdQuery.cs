using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.HR.EmployeeMaster;
using DigitalHub.Domain.Interfaces.HR;

namespace DigitalHub.Application.Services.Master.EmployeeMaster;

public record GetEmployeeMasterByIdQuery
    (long id, CancellationToken cancellationToken = default) : IQuery<ApiResponse>;

public class GetEmployeeMasterByIdQueryHandler(IEmployeeMasterRepository _repository) : IQueryHandler<GetEmployeeMasterByIdQuery, ApiResponse>
{
    public async Task<ApiResponse> Handle(GetEmployeeMasterByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.id, cancellationToken);

        if (entity == null)
        {
            return ApiResponse.Failure(ApiMessage.ItemNotFound);
        }

        var entityDto = new EmployeeMasterDto()
        {
            Id = entity.Id,
            DepartmentId = entity.DepartmentId,
            RoleId = entity.RoleId,
            EmploymentTypeId = entity.EmploymentTypeId,
            HireDate = entity.HireDate,
            Salary = entity.Salary,
            Currency = entity.Currency,

            Department = entity.GetDepartment.Name,
            Role = entity.GetEmployeeRole?.Name,
            EmploymentType = entity.GetEmploymentType?.Name,

            Title = entity.Title,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            DateOfBirth = entity.DateOfBirth,
            Gender = entity.Gender,
            MaritalStatus = entity.MaritalStatus,

            PhoneNumber = entity.PhoneNumber,
            Email = entity.Email,
            Country = entity.Country,
            City = entity.City,
            Address = entity.Address,
            PostalCode = entity.PostalCode,

            NationalID = entity.NationalID,
            PassportNumber = entity.PassportNumber,
            DriverLicenseNumber = entity.DriverLicenseNumber,

            JobTitle = entity.JobTitle,
            Employer = entity.Employer,
            WorkPhoneNumber = entity.WorkPhoneNumber,
            WorkEmail = entity.WorkEmail,

            LinkedIn = entity.LinkedIn,
            Facebook = entity.Facebook,
            Twitter = entity.Twitter,
            Instagram = entity.Instagram,

            EmergencyContactName = entity.EmergencyContactName,
            EmergencyContactPhoneNumber = entity.EmergencyContactPhoneNumber,
            EmergencyContactRelationship = entity.EmergencyContactRelationship,

            Notes = entity.Notes
        };

        return ApiResponse.Success(data: entityDto);
    }
}
