using Domain.Enums;

namespace Persistence.Seed.ApplicationDb;

public class UserSeed
{
    public required string Email { get; init; }
    public required string Password { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string Patronymic { get; init; }
    public required List<DefaultRole> DefaultRoles { get; init; }

    public static List<UserSeed> GetUsersSeedData() =>
        [
            new UserSeed
            {
                Email = "ipz203_tsos@student.ztu.edu.ua",
                Password = "123456",
                FirstName = "Цвік",
                LastName = "Олександр",
                Patronymic = "Сергійович",
                DefaultRoles = [DefaultRole.Admin]
            }
        ];
}
