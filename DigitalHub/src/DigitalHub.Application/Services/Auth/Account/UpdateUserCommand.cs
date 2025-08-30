using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Auth.Account;
using DigitalHub.Domain.Constants;
using DigitalHub.Domain.Entities.Auth.Identity;
using DigitalHub.Domain.Interfaces.Auth;
using DigitalHub.Domain.Utilities;
using FluentValidation;
using Mapster;

namespace DigitalHub.Application.Services.Auth.Account;

public record UpdateUserCommand
    (long id, UpdateUserDto requestDto, CancellationToken cancellationToken = default) : ICommand<ApiResponse>;

public class UpdateUserCommandHandler(IAccountRepository _repository,
    IAuthRepository _authRepository,
    IRoleMasterRepository _roleMasterRepository) : ICommandHandler<UpdateUserCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        if (request.id != request.requestDto.Id)
        {
            return ApiResponse.Failure(ApiMessage.EntityIdMismatch);
        }

        // Check for duplicate user data
        var duplicateError = await CheckForDuplicateUserData(request.requestDto);
        if (duplicateError != null)
        {
            return duplicateError;
        }

        var entity = await _repository.GetUserByIdAsync(request.id, cancellationToken);
        if (entity == null)
        {
            return ApiResponse.Failure(ApiMessage.ItemNotFound);
        }

        if (entity.Email != request.requestDto.Email)
        {
            entity.EmailConfirmed = false;
        }

        if (entity.PhoneNumber != request.requestDto.PhoneNumber)
        {
            entity.PhoneNumberConfirmed = false;
        }

        entity.FirstName = request.requestDto.FirstName;
        entity.LastName = request.requestDto.LastName;
        entity.Email = request.requestDto.Email;
        entity.PhoneNumber = request.requestDto.PhoneNumber;

        var result = await _repository.UpdateUserAsync(entity, cancellationToken);

        if (result)
        {
            // Update user roles
            await UpdateExistingUserRoles(entity, request.requestDto.Roles, cancellationToken);


            // Send confirmation email
            //await _repository.SendConfirmationEmailAsync(request.requestDto.Email);

            var entityDto = entity.Adapt<UserMasterDto>();

            return ApiResponse.Success(ApiMessage.SuccessfulUpdate, entityDto);
        }

        return ApiResponse.Failure(ApiMessage.FailedUpdate);
    }


    public sealed class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.requestDto.FirstName)
            .NotEmpty()
            .MaximumLength(50);

            RuleFor(x => x.requestDto.LastName)
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





    private async Task<ApiResponse?> CheckForDuplicateUserData(UpdateUserDto requestDto)
    {
        var duplicateUser = await _repository.GetUserByEmailAsync(requestDto.Email);
        if (duplicateUser != null && duplicateUser.Id != requestDto.Id)
        {
            return ApiResponse.Failure(ApiMessage.ItemAlreadyExist);
        }

        duplicateUser = await _repository.GetUserByPhoneNumberAsync(requestDto.PhoneNumber);
        if (duplicateUser != null && duplicateUser.Id != requestDto.Id)
        {
            return ApiResponse.Failure(ApiMessage.ItemAlreadyExist);
        }

        return null;
    }

    private async Task UpdateExistingUserRoles(UserMaster entity, string[]? roles, CancellationToken cancellationToken)
    {
        // Retrieve current user roles
        var currentRoles = (await _roleMasterRepository.GetUserRolesAsync(entity, cancellationToken))?.ToHashSet() ?? new HashSet<string>();

        // Determine roles to add and remove
        var newRoles = roles?.ToHashSet() ?? new HashSet<string> { AuthenticationRoles.NONE };
        var rolesToRemove = currentRoles.Except(newRoles).ToList();
        var rolesToAdd = newRoles.Except(currentRoles).ToList();

        // Remove roles no longer assigned
        foreach (var role in rolesToRemove)
        {
            await _authRepository.RemoveUserRoleAsync(entity, role);
        }

        // Add new roles
        foreach (var role in rolesToAdd)
        {
            await _authRepository.AddUserRoleAsync(entity, role);
        }
    }
}

