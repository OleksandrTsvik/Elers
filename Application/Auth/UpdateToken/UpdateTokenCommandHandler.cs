using Application.Auth.DTOs;
using Application.Common.Interfaces;
using Application.Common.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Application.Auth.UpdateToken;

public class UpdateTokenCommandHandler : ICommandHandler<UpdateTokenCommand, AuthDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IUserContext _userContext;
    private readonly IAuthService _authService;
    private readonly IRefreshTokenErrors _refreshTokenErrors;
    private readonly IUserErrors _userErrors;

    public UpdateTokenCommandHandler(
        IApplicationDbContext context,
        IUserContext userContext,
        IAuthService authService,
        IRefreshTokenErrors refreshTokenErrors,
        IUserErrors userErrors)
    {
        _context = context;
        _userContext = userContext;
        _authService = authService;
        _refreshTokenErrors = refreshTokenErrors;
        _userErrors = userErrors;
    }

    public async Task<Result<AuthDto>> Handle(UpdateTokenCommand request, CancellationToken cancellationToken)
    {
        RefreshToken? refreshToken = await _context.RefreshTokens
            .FirstOrDefaultAsync(
                x => x.Token == request.RefreshToken &&
                    x.UserId == _userContext.UserId,
                cancellationToken);

        if (refreshToken is null || !refreshToken.IsActive)
        {
            return _refreshTokenErrors.InvalidToken();
        }

        _context.RefreshTokens.Remove(refreshToken);

        User? user = await _context.Users
            .Include(x => x.Roles)
                .ThenInclude(x => x.Permissions)
            .FirstOrDefaultAsync(x => x.Id == _userContext.UserId, cancellationToken);

        if (user is null)
        {
            return _userErrors.NotFoundByUserContext();
        }

        AuthDto authDto = _authService.CreateAuthDto(user);
        _authService.AddRefreshToken(user.Id, authDto.RefreshToken);

        await _context.SaveChangesAsync(cancellationToken);

        return authDto;
    }
}
