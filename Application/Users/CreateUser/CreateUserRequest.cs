using Domain.Enums;

namespace Application.Users.CreateUser;

public record CreateUserRequest(
    UserType Type,
    string Email,
    string Password,
    string FirstName,
    string LastName,
    string Patronymic,
    Guid[] RoleIds);
