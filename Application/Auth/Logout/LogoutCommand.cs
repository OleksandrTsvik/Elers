using Application.Common.Messaging;

namespace Application.Auth.Logout;

public record LogoutCommand(string RefreshToken) : ICommand;
