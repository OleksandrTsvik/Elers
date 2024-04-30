using Application.Common.Interfaces;
using Application.Common.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.CreateUser;

public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IPasswordService _passwordService;

    public CreateUserCommandHandler(
        IApplicationDbContext context,
        IPasswordService passwordService)
    {
        _context = context;
        _passwordService = passwordService;
    }

    public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        bool userByEmail = await _context.Users
            .AnyAsync(x => x.Email == request.Email, cancellationToken);

        if (userByEmail)
        {
            return UserErrors.EmailNotUnique();
        }

        List<Role> userRoles = await _context.Roles
            .Where(x => request.RoleIds.Contains(x.Id))
            .ToListAsync(cancellationToken);

        var user = new User
        {
            Email = request.Email,
            PasswordHash = _passwordService.HashPassword(request.Password),
            FirstName = request.FirstName,
            LastName = request.LastName,
            Patronymic = request.Patronymic,
            Roles = userRoles
        };

        _context.Users.Add(user);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
