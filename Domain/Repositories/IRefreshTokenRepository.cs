using Domain.Entities;

namespace Domain.Repositories;

public interface IRefreshTokenRepository
{
    Task<RefreshToken?> GetByTokenAsync(
        string token,
        CancellationToken cancellationToken = default);

    Task<RefreshToken?> GetByTokenAndUserIdAsync(
        string token,
        Guid userId,
        CancellationToken cancellationToken = default);

    Task<List<RefreshToken>> GetInvalidTokensAsync(
        Guid userId,
        CancellationToken cancellationToken = default);

    void Add(RefreshToken refreshToken);

    void Remove(RefreshToken refreshToken);

    void RemoveRange(IEnumerable<RefreshToken> refreshTokens);
}
