using Application.Auth.DTOs;
using Domain.Entities;

namespace Application.Common.Services;

public interface ITokenService
{
    TokenDto GenerateAccessToken(User user);
    TokenDto GenerateRefreshToken();
}
