using Application.Common.Messaging;

namespace Application.Users.CreateUser;

public record CreateUserCommand(
    string Email,
    string Password,
    string FirstName,
    string LastName,
    string Patronymic,
    Guid[] RoleIds) : ICommand;
