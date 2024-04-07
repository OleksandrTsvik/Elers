using Application.Auth.DTOs;
using Application.Common.Interfaces;
using Domain.Entities;

namespace Application.Common.Services;

public class AuthService : IAuthService
{
    private readonly IApplicationDbContext _context;
    private readonly ITokenService _tokenService;

    public AuthService(IApplicationDbContext context, ITokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }

    public AuthDto CreateAuthDto(User user)
    {
        TokenDto accessToken = _tokenService.GenerateAccessToken(user);
        TokenDto refreshToken = _tokenService.GenerateRefreshToken();

        return new AuthDto
        {
            User = new AuthUserDto
            {
                Email = user.Email,
                AvatarUrl = user.AvatarUrl,
                Roles = user.Roles.Select(x => x.Name).ToList(),
                Permissions = user.Roles
                    .SelectMany(x => x.Permissions)
                    .Select(x => x.Name)
                    .Distinct()
                    .ToList()
            },
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }

    public void AddRefreshToken(Guid userId, TokenDto refreshToken)
    {
        var refreshTokenEntity = new RefreshToken()
        {
            UserId = userId,
            Token = refreshToken.Token,
            ExpiryDate = refreshToken.ExpiresDate
        };

        _context.RefreshTokens.Add(refreshTokenEntity);
    }
}
