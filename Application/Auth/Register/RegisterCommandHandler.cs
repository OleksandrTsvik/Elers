using Application.Auth.DTOs;
using Application.Common.Interfaces;
using Application.Common.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Application.Auth.Register;

public class RegisterCommandHandler : ICommandHandler<RegisterCommand, AuthDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IPasswordService _passwordService;
    private readonly IAuthService _authService;
    private readonly IUserErrors _userErrors;

    public RegisterCommandHandler(
        IApplicationDbContext context,
        IPasswordService passwordService,
        IAuthService authService,
        IUserErrors userErrors)
    {
        _context = context;
        _passwordService = passwordService;
        _authService = authService;
        _userErrors = userErrors;
    }

    public async Task<Result<AuthDto>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        User? userByEmail = await _context.Users
            .FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);

        if (userByEmail is not null)
        {
            return _userErrors.EmailNotUnique();
        }

        var user = new User
        {
            Email = request.Email,
            PasswordHash = _passwordService.HashPassword(request.Password)
        };

        _context.Users.Add(user);

        AuthDto authDto = _authService.CreateAuthDto(user);
        _authService.AddRefreshToken(user.Id, authDto.RefreshToken);

        await _context.SaveChangesAsync(cancellationToken);

        return authDto;
    }
}
