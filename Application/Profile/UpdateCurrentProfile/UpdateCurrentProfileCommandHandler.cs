using Application.Common.Interfaces;
using Application.Common.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.Profile.UpdateCurrentProfile;

public class UpdateCurrentProfileCommandHandler : ICommandHandler<UpdateCurrentProfileCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IUserContext _userContext;

    public UpdateCurrentProfileCommandHandler(
        IUnitOfWork unitOfWork,
        IUserRepository userRepository,
        IUserContext userContext)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _userContext = userContext;
    }

    public async Task<Result> Handle(UpdateCurrentProfileCommand request, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetByIdWithRolesAsync(_userContext.UserId, cancellationToken);

        if (user is null)
        {
            return UserErrors.NotFound(_userContext.UserId);
        }

        if (await IsNotUniqueEmail(user.Email, request.Email, cancellationToken))
        {
            return UserErrors.EmailNotUnique();
        }

        user.Email = request.Email;
        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.Patronymic = request.Patronymic;
        user.BirthDate = request.BirthDate;

        _userRepository.Update(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    private async Task<bool> IsNotUniqueEmail(
        string currentEmail,
        string newEmail,
        CancellationToken cancellationToken)
    {
        return currentEmail != newEmail
            && !await _userRepository.IsEmailUniqueAsync(newEmail, cancellationToken);
    }
}
