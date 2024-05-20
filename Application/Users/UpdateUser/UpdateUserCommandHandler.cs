using Application.Common.Interfaces;
using Application.Common.Messaging;
using Application.Common.Services;
using Domain.Entities;
using Domain.Errors;
using Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.UpdateUser;

public class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IPasswordService _passwordService;

    public UpdateUserCommandHandler(
        IApplicationDbContext context,
        IPasswordService passwordService)
    {
        _context = context;
        _passwordService = passwordService;
    }

    public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        User? user = await _context.Users
            .Include(x => x.Roles)
            .FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);

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
            List<Role> newUserRoles = await _context.Roles
                .Where(x => request.RoleIds.Contains(x.Id))
                .ToListAsync(cancellationToken);

            user.Roles = newUserRoles;
        }

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    private async Task<bool> IsNotUniqueEmail(
        string currentEmail,
        string newEmail,
        CancellationToken cancellationToken)
    {
        return currentEmail != newEmail
            && await _context.Users.AnyAsync(x => x.Email == newEmail, cancellationToken);
    }
}
