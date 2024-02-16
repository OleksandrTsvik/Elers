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

    public UpdateTokenCommandHandler(
        IApplicationDbContext context,
        IUserContext userContext,
        IAuthService authService)
    {
        _context = context;
        _userContext = userContext;
        _authService = authService;
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
            return RefreshTokenErrors.InvalidToken();
        }

        _context.RefreshTokens.Remove(refreshToken);

        await _context.SaveChangesAsync(cancellationToken);

        User? user = await _context.Users
            .Include(x => x.Roles)
            .FirstOrDefaultAsync(x => x.Id == _userContext.UserId, cancellationToken);

        if (user is null)
        {
            return UserErrors.NotFoundByUserContext();
        }

        AuthDto authDto = await _authService.CreateAuthDto(user, cancellationToken);

        return authDto;
    }
}
