namespace Application.Users.CreateUser;

public record CreateUserRequest(
    string Email,
    string Password,
    string? FirstName,
    string? LastName,
    string? Patronymic,
    Guid[] RoleIds);
