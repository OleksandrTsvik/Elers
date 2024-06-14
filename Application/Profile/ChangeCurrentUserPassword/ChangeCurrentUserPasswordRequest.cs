namespace Application.Profile.ChangeCurrentUserPassword;

public record ChangeCurrentUserPasswordRequest(
    string CurrentPassword,
    string NewPassword,
    string ConfirmPassword);
