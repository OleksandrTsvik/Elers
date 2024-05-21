using Application.Auth.DTOs;
using Application.Common.Interfaces;
using Application.Common.Messaging;
using Application.Common.Services;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.Auth.Login;

public class LoginCommandHandler : ICommandHandler<LoginCommand, AuthDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IPasswordService _passwordService;
    private readonly IAuthService _authService;

    public LoginCommandHandler(
        IUnitOfWork unitOfWork,
        IUserRepository userRepository,
        IRefreshTokenRepository refreshTokenRepository,
        IPasswordService passwordService,
        IAuthService authService)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _passwordService = passwordService;
        _authService = authService;
    }

    public async Task<Result<AuthDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetByEmailWithRolesAndPermissionsAsync(
            request.Email, cancellationToken);

        if (user is null)
        {
            return UserErrors.InvalidCredentials();
        }

        if (!_passwordService.VerifyHashedPassword(request.Password, user.PasswordHash))
        {
            return UserErrors.InvalidCredentials();
        }

        List<RefreshToken> invalidRefreshTokens = await _refreshTokenRepository
            .GetInvalidTokensAsync(user.Id, cancellationToken);

        _refreshTokenRepository.RemoveRange(invalidRefreshTokens);

        AuthDto authDto = _authService.CreateAuthDto(user);
        _authService.AddRefreshToken(user.Id, authDto.RefreshToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return authDto;
    }
}
