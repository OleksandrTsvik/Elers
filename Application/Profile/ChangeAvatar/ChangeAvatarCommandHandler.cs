using Application.Common.Interfaces;
using Application.Common.Messaging;
using Application.Common.Models;
using Application.Common.Services;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.Profile.ChangeAvatar;

public class ChangeAvatarCommandHandler : ICommandHandler<ChangeAvatarCommand, string>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IImageService _imageService;
    private readonly IUserContext _userContext;

    public ChangeAvatarCommandHandler(
        IUnitOfWork unitOfWork,
        IUserRepository userRepository,
        IImageService imageService,
        IUserContext userContext)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _imageService = imageService;
        _userContext = userContext;
    }

    public async Task<Result<string>> Handle(ChangeAvatarCommand request, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetByIdAsync(_userContext.UserId, cancellationToken);

        if (user is null)
        {
            return UserErrors.NotFound(_userContext.UserId);
        }

        if (user.AvatarImageName is not null)
        {
            await _imageService.RemoveAsync(user.AvatarImageName, cancellationToken);
        }

        Result<ImageUploadResult> addImageResult = await _imageService.AddAsync(
            request.Avatar, cancellationToken);

        if (addImageResult.IsFailure || addImageResult.Value is null)
        {
            return addImageResult.Error;
        }

        user.AvatarUrl = addImageResult.Value.Url;
        user.AvatarImageName = addImageResult.Value.UniqueName;

        _userRepository.Update(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return user.AvatarUrl;
    }
}
