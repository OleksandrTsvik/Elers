using Application.Auth.DTOs;
using Application.Common.Messaging;

namespace Application.Auth.Register;

public record RegisterCommand(string Email, string Password) : ICommand<AuthDto>;
