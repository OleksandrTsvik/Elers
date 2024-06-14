namespace Application.Profile.UpdateCurrentProfile;

public record UpdateCurrentProfileRequest(
    string Email,
    string FirstName,
    string LastName,
    string Patronymic,
    DateTime? BirthDate);
