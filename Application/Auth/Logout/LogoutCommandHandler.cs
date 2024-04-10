using Application.Common.Interfaces;
using Application.Common.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Application.Auth.Logout;

public class LogoutCommandHandler : ICommandHandler<LogoutCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IUserContext _userContext;
    private readonly IRefreshTokenErrors _refreshTokenErrors;

    public LogoutCommandHandler(
        IApplicationDbContext context,
        IUserContext userContext,
        IRefreshTokenErrors refreshTokenErrors)
    {
        _context = context;
        _userContext = userContext;
        _refreshTokenErrors = refreshTokenErrors;
    }

    public async Task<Result> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        RefreshToken? refreshToken = await _context.RefreshTokens
            .FirstOrDefaultAsync(
                x => x.Token == request.RefreshToken &&
                    x.UserId == _userContext.UserId,
                cancellationToken);

        if (refreshToken is null)
        {
            return _refreshTokenErrors.InvalidToken();
        }

        _context.RefreshTokens.Remove(refreshToken);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
