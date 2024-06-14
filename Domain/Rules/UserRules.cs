namespace Domain.Rules;

public static class UserRules
{
    public const int MaxEmailLength = 64;

    public const int MinPasswordLength = 6;
    public const int MaxPasswordLength = 32;

    public const int MaxPasswordHashLength = 512;

    public const int MinFirstNameLength = 1;
    public const int MaxFirstNameLength = 64;

    public const int MinLastNameLength = 1;
    public const int MaxLastNameLength = 64;

    public const int MinPatronymicLength = 1;
    public const int MaxPatronymicLength = 64;

    public const int MaxAvatarUrlLength = 2048;
    public const int MaxAvatarImageNameLength = 256;
}
