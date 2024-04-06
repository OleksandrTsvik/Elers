using Application.Auth.DTOs;
using Application.Common.Interfaces;
using Application.Common.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Application.Auth.Login;

public class LoginCommandHandler : ICommandHandler<LoginCommand, AuthDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IPasswordService _passwordService;
    private readonly IAuthService _authService;

    public LoginCommandHandler(
        IApplicationDbContext context,
        IPasswordService passwordService,
        IAuthService authService)
    {
        _context = context;
        _passwordService = passwordService;
        _authService = authService;
    }

    public async Task<Result<AuthDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        User? user = await _context.Users
            .Include(x => x.Roles)
                .ThenInclude(x => x.Permissions)
            .FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);

        if (user is null)
        {
            return UserErrors.InvalidCredentials();
        }

        if (!_passwordService.VerifyHashedPassword(request.Password, user.PasswordHash))
        {
            return UserErrors.InvalidCredentials();
        }

        AuthDto authDto = await _authService.CreateAuthDto(user, cancellationToken);

        return authDto;
    }
}
