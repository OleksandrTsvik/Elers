using Application.Common.Interfaces;
using Domain.Constants;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Seed;

public class ApplicationDbContextSeed
{
    private readonly ApplicationDbContext _context;
    private readonly IPasswordService _passwordService;

    public ApplicationDbContextSeed(ApplicationDbContext context, IPasswordService passwordService)
    {
        _context = context;
        _passwordService = passwordService;
    }

    public async Task SeedDataAsync()
    {
        List<Permission> allPermissions = await GetAndSeedPermissionsAsync();
        List<Role> allRoles = await GetAndSeedRolesAsync(allPermissions);

        await SeedUsersAsync(allRoles);

        await _context.SaveChangesAsync();
    }

    private async Task<List<Permission>> GetAndSeedPermissionsAsync()
    {
        List<Permission> existingPermissionsInDb = await _context.Permissions.ToListAsync();

        var removePermissionsFromDb = new List<Permission>();
        var existingPermissions = new List<Permission>();

        foreach (Permission permission in existingPermissionsInDb)
        {
            if (PermissionsSetup.AllPermissions.ContainsPermission(permission.Name))
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
            string permissionName = permissionType.ToString();

            if (!existingPermissions.Any(x => x.Name == permissionName))
            {
                newPermissions.Add(new Permission { Name = permissionName });
            }
        }

        _context.Permissions.RemoveRange(removePermissionsFromDb);
        await _context.Permissions.AddRangeAsync(newPermissions);

        return existingPermissions.Concat(newPermissions).ToList();
    }

    private async Task<List<Role>> GetAndSeedRolesAsync(List<Permission> allPermissions)
    {
        List<Role> existingRoles = await _context.Roles
            .Include(x => x.Permissions)
            .ToListAsync();

        var newRoles = new List<Role>();

        foreach (DefaultRole role in PermissionsSetup.DefaultRolePermissions.Keys)
        {
            Role? currentRole = existingRoles.Find(x => x.Name == role.ToString());

            var rolePermissions = allPermissions
                .Where(x => PermissionsSetup.DefaultRolePermissions[role]
                    .ContainsPermission(x.Name))
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
            else
            {
                currentRole.Permissions = rolePermissions;
            }
        }

        await _context.Roles.AddRangeAsync(newRoles);

        return existingRoles.Concat(newRoles).ToList();
    }

    private async Task SeedUsersAsync(List<Role> allRoles)
    {
        List<UserDto> seedUsers = GetUsersSeedData();
        var seedEmails = seedUsers.Select(x => x.Email).ToList();

        List<string> existingEmails = await _context.Users
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
                    RegistrationDate = DateTime.UtcNow,
                    Roles = roles
                };

                newUsers.Add(newUser);
            }
        }

        await _context.Users.AddRangeAsync(newUsers);
    }

    private static List<UserDto> GetUsersSeedData() =>
        [
            new UserDto
            {
                Email = "ipz203_tsos@student.ztu.edu.ua",
                Password = "123456",
                DefaultRoles = [DefaultRole.Admin]
            }
        ];
}
