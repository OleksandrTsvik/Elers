namespace Application.Profile.GetCurrentProfile;

public class GetCurrentProfileResponse
{
    public required string Email { get; init; }
    public required DateTime RegistrationDate { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string Patronymic { get; init; }
    public required string? AvatarUrl { get; init; }
    public required DateTime? BirthDate { get; init; }
}
