using Application.Common.Interfaces;
using Application.Common.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.Auth.Logout;

public class LogoutCommandHandler : ICommandHandler<LogoutCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IUserContext _userContext;

    public LogoutCommandHandler(
        IUnitOfWork unitOfWork,
        IRefreshTokenRepository refreshTokenRepository,
        IUserContext userContext)
    {
        _unitOfWork = unitOfWork;
        _refreshTokenRepository = refreshTokenRepository;
        _userContext = userContext;
    }

    public async Task<Result> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        RefreshToken? refreshToken = await _refreshTokenRepository.GetByTokenAndUserIdAsync(
            request.RefreshToken, _userContext.UserId, cancellationToken);

        if (refreshToken is null)
        {
            return RefreshTokenErrors.InvalidToken();
        }

        _refreshTokenRepository.Remove(refreshToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
