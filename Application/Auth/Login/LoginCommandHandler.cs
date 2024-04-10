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
    private readonly IUserErrors _userErrors;

    public LoginCommandHandler(
        IApplicationDbContext context,
        IPasswordService passwordService,
        IAuthService authService,
        IUserErrors userErrors)
    {
        _context = context;
        _passwordService = passwordService;
        _authService = authService;
        _userErrors = userErrors;
    }

    public async Task<Result<AuthDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        User? user = await _context.Users
            .Include(x => x.Roles)
                .ThenInclude(x => x.Permissions)
            .FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);

        if (user is null)
        {
            return _userErrors.InvalidCredentials();
        }

        if (!_passwordService.VerifyHashedPassword(request.Password, user.PasswordHash))
        {
            return _userErrors.InvalidCredentials();
        }

        List<RefreshToken> invalidRefreshTokens = await _context.RefreshTokens
            .Where(x => x.UserId == user.Id &&
                (x.RevokedDate != null || DateTime.UtcNow >= x.ExpiryDate))
            .ToListAsync(cancellationToken);

        _context.RefreshTokens.RemoveRange(invalidRefreshTokens);

        AuthDto authDto = _authService.CreateAuthDto(user);
        _authService.AddRefreshToken(user.Id, authDto.RefreshToken);

        await _context.SaveChangesAsync(cancellationToken);

        return authDto;
    }
}
