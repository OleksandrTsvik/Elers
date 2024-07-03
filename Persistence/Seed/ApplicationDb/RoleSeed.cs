namespace Persistence.Seed.ApplicationDb;

public static class RoleSeed
{
    public static string GetRoleName(DefaultRole defaultRole) =>
        defaultRole switch
        {
            DefaultRole.Admin => "Адміністратор",
            DefaultRole.Author => "Автор",
            _ => defaultRole.ToString()
        };
}
