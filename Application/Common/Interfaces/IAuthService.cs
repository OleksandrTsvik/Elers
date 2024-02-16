using Application.Auth.DTOs;
using Domain.Entities;

namespace Application.Common.Interfaces;

public interface IAuthService
{
    Task<AuthDto> CreateAuthDto(User user, CancellationToken cancellationToken);
}
