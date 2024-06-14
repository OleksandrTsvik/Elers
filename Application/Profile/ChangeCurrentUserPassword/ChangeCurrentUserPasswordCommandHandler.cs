using Application.Common.Interfaces;
using Application.Common.Messaging;
using Application.Common.Services;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.Profile.ChangeCurrentUserPassword;

public class ChangeCurrentUserPasswordCommandHandler : ICommandHandler<ChangeCurrentUserPasswordCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordService _passwordService;
    private readonly IUserContext _userContext;

    public ChangeCurrentUserPasswordCommandHandler(
        IUnitOfWork unitOfWork,
        IUserRepository userRepository,
        IPasswordService passwordService,
        IUserContext userContext)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _passwordService = passwordService;
        _userContext = userContext;
    }

    public async Task<Result> Handle(
        ChangeCurrentUserPasswordCommand request,
        CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetByIdWithRolesAsync(_userContext.UserId, cancellationToken);

        if (user is null)
        {
            return UserErrors.NotFound(_userContext.UserId);
        }

        if (!_passwordService.VerifyHashedPassword(request.CurrentPassword, user.PasswordHash))
        {
            return UserErrors.InvalidPassword();
        }

        user.PasswordHash = _passwordService.HashPassword(request.NewPassword);

        _userRepository.Update(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
