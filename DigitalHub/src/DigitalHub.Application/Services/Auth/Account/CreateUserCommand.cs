using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Auth.Account;
using DigitalHub.Application.Services.Auth.Common;
using DigitalHub.Domain.Constants;
using DigitalHub.Domain.Entities.Auth.Identity;
using DigitalHub.Domain.Enums;
using DigitalHub.Domain.Interfaces.Auth;
using DigitalHub.Domain.Interfaces.Common;
using DigitalHub.Domain.Utilities;
using FluentValidation;
using Mapster;
using Microsoft.AspNetCore.Http;

namespace DigitalHub.Application.Services.Auth.Account;

public record CreateUserCommand
    (CreateUserDto requestDto, CancellationToken cancellationToken = default) : ICommand<ApiResponse>;

public class CreateUserCommandHandler(IAccountRepository _repository,
    IAuthRepository _authRepository,
    IRoleMasterRepository _roleRepository,
    IHttpContextAccessor _httpContextAccessor,
    ICompanyProfileSettingsRepository _companyProfileSettings,
    IEmailSender _emailSender) : ICommandHandler<CreateUserCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        // Check for duplicate user data
        var duplicateError = await CheckForDuplicateUserData(request.requestDto);
        if (duplicateError != null)
        {
            return duplicateError;
        }

        await _repository.UnitOfWork.BeginTransactionAsync(cancellationToken);

        var user = request.requestDto.Adapt<UserMaster>();
        var result = await _repository.CreateUserAsync(user, request.requestDto.Password, cancellationToken);
        if (result)
        {
            // Assign roles
            await AssignRolesToUser(user, request.requestDto.UserType, cancellationToken, request.requestDto.Roles);

            // Send confirmation email
            var token = await _repository.GenerateEmailConfirmationTokenAsync(user);
            var companyProfile = await _companyProfileSettings.GetSettingsAsync(cancellationToken);
            await AuthHelper.SendConfirmationEmailAsync(user, companyProfile!, _httpContextAccessor.HttpContext!.Request, _emailSender, token, cancellationToken);


            var entityDto = user.Adapt<UserMasterDto>();

            await _repository.UnitOfWork.CommitTransactionAsync(cancellationToken);
            return ApiResponse.Success(ApiMessage.SuccessfulCreate, entityDto);
        }

        await _repository.UnitOfWork.RollbackTransactionAsync(cancellationToken);
        return ApiResponse.Failure(ApiMessage.FailedCreate);
    }

    public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.requestDto.FirstName)
            .NotEmpty()
            .MaximumLength(50);

            RuleFor(x => x.requestDto.LastName)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.requestDto.UserName)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.requestDto.Email)
                .NotEmpty()
                .MaximumLength(50)
                .EmailAddress();

            RuleFor(x => x.requestDto.PhoneNumber)
                .NotEmpty()
                .MaximumLength(20)
                .Matches(ExpressionHelper.PhoneNumber)
                .WithMessage("Invalid phone number format");
        }
    }






    private async Task<ApiResponse?> CheckForDuplicateUserData(CreateUserDto requestDto)
    {
        if (await _repository.GetUserByNameAsync(requestDto.UserName) != null)
        {
            return ApiResponse.Failure(ApiMessage.ItemAlreadyExist);
        }
        else if (await _repository.GetUserByEmailAsync(requestDto.Email) != null)
        {
            return ApiResponse.Failure(ApiMessage.ItemAlreadyExist);
        }
        else if (await _repository.GetUserByPhoneNumberAsync(requestDto.PhoneNumber) != null)
        {
            return ApiResponse.Failure(ApiMessage.ItemAlreadyExist);
        }

        return null;
    }

    private async Task AssignRolesToUser(UserMaster entity, UserTypes userType, CancellationToken cancellationToken, string[]? roles)
    {
        if (roles != null && roles.Any())
        {
            foreach (var role in roles)
            {
                await _authRepository.AddUserRoleAsync(entity, role);
            }
        }
        else
        {
            var defaultRoles = await _roleRepository.GetDefaultRolesAsync(userType, cancellationToken);
            if (defaultRoles != null && defaultRoles.Any())
            {
                foreach (var role in defaultRoles)
                {
                    await _authRepository.AddUserRoleAsync(entity, role.Name!);
                }
            }
            else
            {
                await _authRepository.AddUserRoleAsync(entity, AuthenticationRoles.NONE);
            }
        }
    }


}