using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application.Auth.DTOs;
using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Authentication;

public class TokenService : ITokenService
{
    private readonly JwtOptions _jwtOptions;

    public TokenService(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }

    public TokenDto GenerateAccessToken(User user)
    {
        var claims = new Claim[]
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email)
        };

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)),
            SecurityAlgorithms.HmacSha256);

        DateTime expiresDate = DateTime.UtcNow.AddMinutes(_jwtOptions.AccessTokenExpirationInMinutes);

        var securityToken = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: claims,
            expires: expiresDate,
            signingCredentials: signingCredentials);

        string token = new JwtSecurityTokenHandler()
            .WriteToken(securityToken);

        return new TokenDto { Token = token, ExpiresDate = expiresDate };
    }

    public TokenDto GenerateRefreshToken()
    {
        byte[] randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();

        rng.GetBytes(randomNumber);

        string token = Convert.ToBase64String(randomNumber);
        DateTime expiresDate = DateTime.UtcNow.AddDays(_jwtOptions.RefreshTokenExpirationInDays);

        return new TokenDto { Token = token, ExpiresDate = expiresDate };
    }
}
