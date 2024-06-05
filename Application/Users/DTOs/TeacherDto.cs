namespace Application.Users.DTOs;

public class TeacherDto
{
    public required Guid Id { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string Patronymic { get; init; }
}
