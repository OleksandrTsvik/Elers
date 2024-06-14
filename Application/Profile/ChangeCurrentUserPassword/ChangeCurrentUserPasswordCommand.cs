using Application.Common.Messaging;

namespace Application.Profile.ChangeCurrentUserPassword;

public record ChangeCurrentUserPasswordCommand(
    string CurrentPassword,
    string NewPassword,
    string ConfirmPassword) : ICommand;
