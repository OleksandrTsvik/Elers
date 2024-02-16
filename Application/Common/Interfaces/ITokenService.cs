using Application.Auth.DTOs;
using Domain.Entities;

namespace Application.Common.Interfaces;

public interface ITokenService
{
    TokenDto GenerateAccessToken(User user);
    string GenerateRefreshToken();
}
