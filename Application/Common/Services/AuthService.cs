using Application.Auth.DTOs;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Common.Services;

public class AuthService : IAuthService
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly ITokenService _tokenService;

    public AuthService(IRefreshTokenRepository refreshTokenRepository, ITokenService tokenService)
    {
        _refreshTokenRepository = refreshTokenRepository;
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
                Type = user.Type,
                Email = user.Email,
                AvatarUrl = user.AvatarUrl,
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

        _refreshTokenRepository.Add(refreshTokenEntity);
    }
}
