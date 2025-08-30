using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Auth.Account;
using DigitalHub.Domain.Extensions;
using DigitalHub.Domain.Interfaces.Auth;
using DigitalHub.Domain.Interfaces.Common;
using DigitalHub.Domain.Utilities;
using FluentValidation;
using Mapster;

namespace DigitalHub.Application.Services.Auth.Account;

public record UpdateUserProfileCommand
    (UpdateUserProfileDto requestDto, CancellationToken cancellationToken = default) : ICommand<ApiResponse>;

public class UpdateUserProfileCommandHandler(IAccountRepository _repository, ICurrentUser _currentUser, IPathFinderService _pathFinderService) : ICommandHandler<UpdateUserProfileCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
    {
        long userId = _currentUser.UserId;
        if (userId != request.requestDto.Id)
        {
            return ApiResponse.Failure(ApiMessage.EntityIdMismatch);
        }

        // Check for duplicate user data
        var duplicateError = await CheckForDuplicateUserData(request.requestDto);
        if (duplicateError != null)
        {
            return duplicateError;
        }

        var entity = await _repository.GetUserByIdAsync(userId, cancellationToken);
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

        if (request.requestDto.ProfileImage != null)
        {
            var fileName = request.requestDto.ProfileImage.GenerateUniqueFileName();
            await request.requestDto.ProfileImage.UploadToAsync(_pathFinderService.UserProfileFolderPath, fileName);
            entity.ProfileImage = fileName;
        }

        var result = await _repository.UpdateUserAsync(entity, cancellationToken);

        if (result)
        {
            var entityDto = entity.Adapt<UserMasterDto>();

            return ApiResponse.Success(ApiMessage.SuccessfulUpdate, entityDto);
        }

        return ApiResponse.Failure(ApiMessage.FailedUpdate);
    }

    public sealed class UpdateUserProfileCommandValidator : AbstractValidator<UpdateUserProfileCommand>
    {
        public UpdateUserProfileCommandValidator()
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







    private async Task<ApiResponse?> CheckForDuplicateUserData(UpdateUserProfileDto requestDto)
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
}

