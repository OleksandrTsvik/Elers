using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

internal class RefreshTokenRepository : ApplicationDbRepository<RefreshToken>, IRefreshTokenRepository
{
    public RefreshTokenRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }

    public Task<RefreshToken?> GetByTokenAndUserIdAsync(
        string token,
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        return DbContext.RefreshTokens
            .FirstOrDefaultAsync(x => x.Token == token && x.UserId == userId, cancellationToken);
    }

    public Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken cancellationToken = default)
    {
        return DbContext.RefreshTokens
            .FirstOrDefaultAsync(x => x.Token == token, cancellationToken);
    }

    public Task<List<RefreshToken>> GetInvalidTokensAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        return DbContext.RefreshTokens
            .Where(x => x.UserId == userId &&
                (x.RevokedDate != null || DateTime.UtcNow >= x.ExpiryDate))
            .ToListAsync(cancellationToken);
    }
}
