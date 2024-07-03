using Persistence.Options;

namespace Persistence.Seed.ApplicationDb;

public class UserSeed
{
    public required string Email { get; init; }
    public required string Password { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string Patronymic { get; init; }
    public required List<DefaultRole> DefaultRoles { get; init; }

    public static List<UserSeed> GetUsersSeedData(SeedOptions seedOptions) =>
        [
            new UserSeed
            {
                Email = seedOptions.AdminEmail,
                Password = seedOptions.AdminPassword,
                FirstName = "Олександр",
                LastName = "Цвік",
                Patronymic = "Сергійович",
                DefaultRoles = [DefaultRole.Admin]
            }
        ];
}
