using Application.Common.Messaging;
using Domain.Enums;

namespace Application.Users.CreateUser;

public record CreateUserCommand(
    UserType Type,
    string Email,
    string Password,
    string FirstName,
    string LastName,
    string Patronymic,
    Guid[] RoleIds) : ICommand;
