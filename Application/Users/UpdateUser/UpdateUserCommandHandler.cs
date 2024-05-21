using Application.Common.Interfaces;
using Application.Common.Messaging;
using Application.Common.Services;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.Users.UpdateUser;

public class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IPasswordService _passwordService;

    public UpdateUserCommandHandler(
        IUnitOfWork unitOfWork,
        IUserRepository userRepository,
        IRoleRepository roleRepository,
        IPasswordService passwordService)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _passwordService = passwordService;
    }

    public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetByIdWithRolesAsync(request.UserId, cancellationToken);

        if (user is null)
        {
            return UserErrors.NotFound(request.UserId);
        }

        if (await IsNotUniqueEmail(user.Email, request.Email, cancellationToken))
        {
            return UserErrors.EmailNotUnique();
        }

        user.Email = request.Email;
        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.Patronymic = request.Patronymic;

        if (!string.IsNullOrEmpty(request.Password))
        {
            user.PasswordHash = _passwordService.HashPassword(request.Password);
        }

        if (request.RoleIds.Length == 0)
        {
            user.Roles.Clear();
        }
        else
        {
            user.Roles = await _roleRepository.GetListAsync(request.RoleIds, cancellationToken);
        }

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
