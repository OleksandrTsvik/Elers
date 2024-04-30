using Application.Common.Messaging;

namespace Application.Users.DeleteUser;

public record DeleteUserCommand(Guid UserId) : ICommand;
