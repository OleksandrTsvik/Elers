namespace Application.Users.UpdateUser;

public record UpdateUserRequest(
    string Email,
    string? Password,
    string FirstName,
    string LastName,
    string Patronymic,
    Guid[] RoleIds);
