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
    private readonly IAuthService _authService;

    public UpdateTokenCommandHandler(
        IApplicationDbContext context,
        IAuthService authService)
    {
        _context = context;
        _authService = authService;
    }

    public async Task<Result<AuthDto>> Handle(UpdateTokenCommand request, CancellationToken cancellationToken)
    {
        RefreshToken? refreshToken = await _context.RefreshTokens
            .FirstOrDefaultAsync(x => x.Token == request.RefreshToken, cancellationToken);

        if (refreshToken is null || !refreshToken.IsActive)
        {
            return RefreshTokenErrors.InvalidToken();
        }

        _context.RefreshTokens.Remove(refreshToken);

        User? user = await _context.Users
            .Include(x => x.Roles)
                .ThenInclude(x => x.Permissions)
            .FirstOrDefaultAsync(x => x.Id == refreshToken.UserId, cancellationToken);

        if (user is null)
        {
            return UserErrors.NotFound(refreshToken.UserId);
        }

        AuthDto authDto = _authService.CreateAuthDto(user);
        _authService.AddRefreshToken(user.Id, authDto.RefreshToken);

        await _context.SaveChangesAsync(cancellationToken);

        return authDto;
    }
}
