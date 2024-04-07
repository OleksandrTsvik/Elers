using Application.Auth.DTOs;
using Domain.Entities;

namespace Application.Common.Interfaces;

public interface IAuthService
{
    AuthDto CreateAuthDto(User user);
    void AddRefreshToken(Guid userId, TokenDto refreshToken);
}
