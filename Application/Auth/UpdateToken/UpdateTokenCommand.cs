using Application.Auth.DTOs;
using Application.Common.Messaging;

namespace Application.Auth.UpdateToken;

public record UpdateTokenCommand(string RefreshToken) : ICommand<AuthDto>;
