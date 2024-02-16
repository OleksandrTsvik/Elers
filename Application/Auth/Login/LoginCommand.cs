using Application.Auth.DTOs;
using Application.Common.Messaging;

namespace Application.Auth.Login;

public record LoginCommand(string Email, string Password) : ICommand<AuthDto>;
