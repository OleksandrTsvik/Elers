using Application.Common.Services;
using Domain.Constants;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Seed.ApplicationDb;

public class ApplicationDbContextSeed
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IPasswordService _passwordService;

    public ApplicationDbContextSeed(ApplicationDbContext dbContext, IPasswordService passwordService)
    {
        _dbContext = dbContext;
        _passwordService = passwordService;
    }

    public async Task SeedDataAsync(bool isDevelopment)
    {
        List<Permission> allPermissions = await GetAndSeedPermissionsAsync();
        List<Role> allRoles = await GetAndSeedRolesAsync(isDevelopment, allPermissions);

        await SeedUsersAsync(allRoles);

        await SeedCoursePermissionsAsync();

        await _dbContext.SaveChangesAsync();
    }

    private async Task<List<Permission>> GetAndSeedPermissionsAsync()
    {
        List<Permission> existingPermissionsInDb = await _dbContext.Permissions.ToListAsync();

        var removePermissionsFromDb = new List<Permission>();
        var existingPermissions = new List<Permission>();

        foreach (Permission permission in existingPermissionsInDb)
        {
            if (PermissionsSetup.AllPermissions.Contains(permission.Name))
            {
                existingPermissions.Add(permission);
            }
            else
            {
                removePermissionsFromDb.Add(permission);
            }
        }

        var newPermissions = new List<Permission>();

        foreach (PermissionType permissionType in PermissionsSetup.AllPermissions)
        {
            if (!existingPermissions.Any(x => x.Name == permissionType))
            {
                newPermissions.Add(new Permission { Name = permissionType });
            }
        }

        _dbContext.Permissions.RemoveRange(removePermissionsFromDb);
        await _dbContext.Permissions.AddRangeAsync(newPermissions);

        return existingPermissions.Concat(newPermissions).ToList();
    }

    private async Task<List<Role>> GetAndSeedRolesAsync(bool isDevelopment, List<Permission> allPermissions)
    {
        List<Role> existingRoles = await _dbContext.Roles
            .Include(x => x.Permissions)
            .ToListAsync();

        var newRoles = new List<Role>();

        foreach (DefaultRole role in PermissionsSetup.DefaultRolePermissions.Keys)
        {
            Role? currentRole = existingRoles.Find(x => x.Name == role.ToString());

            var rolePermissions = allPermissions
                .Where(x => PermissionsSetup.DefaultRolePermissions[role].Contains(x.Name))
                .ToList();

            if (currentRole is null)
            {
                var newRole = new Role
                {
                    Name = role.ToString(),
                    Permissions = rolePermissions
                };

                newRoles.Add(newRole);
            }
            else if (isDevelopment)
            {
                currentRole.Permissions = rolePermissions;
            }
        }

        await _dbContext.Roles.AddRangeAsync(newRoles);

        return existingRoles.Concat(newRoles).ToList();
    }

    private async Task SeedUsersAsync(List<Role> allRoles)
    {
        List<UserDto> seedUsers = GetUsersSeedData();
        var seedEmails = seedUsers.Select(x => x.Email).ToList();

        List<string> existingEmails = await _dbContext.Users
            .Where(x => seedEmails.Contains(x.Email))
            .Select(x => x.Email)
            .ToListAsync();

        var newUsers = new List<User>();

        foreach (UserDto user in seedUsers)
        {
            if (!existingEmails.Any(x => x == user.Email))
            {
                string passwordHash = _passwordService.HashPassword(user.Password);

                var userDefaultRoles = user.DefaultRoles.Select(x => x.ToString()).ToList();
                var roles = allRoles
                    .Where(x => userDefaultRoles.Contains(x.Name))
                    .ToList();

                var newUser = new User
                {
                    Email = user.Email,
                    PasswordHash = passwordHash,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Patronymic = user.Patronymic,
                    Roles = roles
                };

                newUsers.Add(newUser);
            }
        }

        await _dbContext.Users.AddRangeAsync(newUsers);
    }

    private async Task SeedCoursePermissionsAsync()
    {
        CoursePermissionType[] allPermissions = Enum.GetValues<CoursePermissionType>();

        List<CoursePermission> existingPermissionsInDb = await _dbContext.CoursePermissions
            .ToListAsync();

        var removePermissionsFromDb = new List<CoursePermission>();
        var existingPermissions = new List<CoursePermission>();

        foreach (CoursePermission permission in existingPermissionsInDb)
        {
            if (allPermissions.Contains(permission.Name))
            {
                existingPermissions.Add(permission);
            }
            else
            {
                removePermissionsFromDb.Add(permission);
            }
        }

        var newPermissions = new List<CoursePermission>();

        foreach (CoursePermissionType permissionType in allPermissions)
        {
            if (!existingPermissions.Any(x => x.Name == permissionType))
            {
                newPermissions.Add(new CoursePermission { Name = permissionType });
            }
        }

        _dbContext.CoursePermissions.RemoveRange(removePermissionsFromDb);
        await _dbContext.CoursePermissions.AddRangeAsync(newPermissions);
    }

    private static List<UserDto> GetUsersSeedData() =>
        [
            new UserDto
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
