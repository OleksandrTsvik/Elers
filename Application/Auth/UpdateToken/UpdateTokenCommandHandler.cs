using Application.Auth.DTOs;
using Application.Common.Interfaces;
using Application.Common.Messaging;
using Application.Common.Services;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.Auth.UpdateToken;

public class UpdateTokenCommandHandler : ICommandHandler<UpdateTokenCommand, AuthDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IAuthService _authService;

    public UpdateTokenCommandHandler(
        IUnitOfWork unitOfWork,
        IUserRepository userRepository,
        IRefreshTokenRepository refreshTokenRepository,
        IAuthService authService)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _authService = authService;
    }

    public async Task<Result<AuthDto>> Handle(UpdateTokenCommand request, CancellationToken cancellationToken)
    {
        RefreshToken? refreshToken = await _refreshTokenRepository.GetByTokenAsync(
            request.RefreshToken, cancellationToken);

        if (refreshToken is null || !refreshToken.IsActive)
        {
            return RefreshTokenErrors.InvalidToken();
        }

        _refreshTokenRepository.Remove(refreshToken);

        User? user = await _userRepository.GetByIdWithRolesAndPermissionsAsync(
            refreshToken.UserId, cancellationToken);

        if (user is null)
        {
            return UserErrors.NotFound(refreshToken.UserId);
        }

        AuthDto authDto = _authService.CreateAuthDto(user);
        _authService.AddRefreshToken(user.Id, authDto.RefreshToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return authDto;
    }
}
