using Application.Common.Messaging;

namespace Application.Profile.UpdateCurrentProfile;

public record UpdateCurrentProfileCommand(
    string Email,
    string FirstName,
    string LastName,
    string Patronymic,
    DateTime? BirthDate) : ICommand;
