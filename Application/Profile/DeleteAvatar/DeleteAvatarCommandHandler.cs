using Application.Common.Interfaces;
using Application.Common.Messaging;
using Application.Common.Services;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.Profile.DeleteAvatar;

public class DeleteAvatarCommandHandler : ICommandHandler<DeleteAvatarCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IImageService _imageService;
    private readonly IUserContext _userContext;

    public DeleteAvatarCommandHandler(
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

    public async Task<Result> Handle(DeleteAvatarCommand request, CancellationToken cancellationToken)
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

        user.AvatarUrl = null;
        user.AvatarImageName = null;

        _userRepository.Update(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
