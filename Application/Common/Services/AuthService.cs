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

    public async Task<AuthDto> CreateAuthDto(User user, CancellationToken cancellationToken)
    {
        TokenDto accessToken = _tokenService.GenerateAccessToken(user);
        string refreshToken = _tokenService.GenerateRefreshToken();

        RefreshToken refreshTokenEntity = new RefreshToken
        {
            UserId = user.Id,
            Token = refreshToken
        };

        _context.RefreshTokens.Add(refreshTokenEntity);

        await _context.SaveChangesAsync(cancellationToken);

        return new AuthDto
        {
            User = new AuthUserDto
            {
                Email = user.Email,
                AvatarUrl = user.AvatarUrl,
                Roles = user.Roles.Select(x => x.Name).ToList()
            },
            AccessToken = accessToken,
            RefreshToken = new TokenDto
            {
                Token = refreshTokenEntity.Token,
                ExpiresDate = refreshTokenEntity.ExpiresDate
            }
        };
    }
}
