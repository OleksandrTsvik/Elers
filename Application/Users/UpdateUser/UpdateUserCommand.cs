using Application.Common.Messaging;

namespace Application.Users.UpdateUser;

public record UpdateUserCommand(
    Guid UserId,
    string Email,
    string? Password,
    string? FirstName,
    string? LastName,
    string? Patronymic,
    Guid[] RoleIds) : ICommand;
