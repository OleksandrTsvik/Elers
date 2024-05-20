using Application.Auth.DTOs;
using Domain.Entities;

namespace Application.Common.Services;

public interface IAuthService
{
    AuthDto CreateAuthDto(User user);
    void AddRefreshToken(Guid userId, TokenDto refreshToken);
}
