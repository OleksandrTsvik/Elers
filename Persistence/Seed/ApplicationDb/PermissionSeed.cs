using Domain.Enums;

namespace Persistence.Seed.ApplicationDb;

public static class PermissionSeed
{
    public static readonly PermissionType[] AllPermissions = Enum.GetValues<PermissionType>();

    private static readonly PermissionType[] AdminPermissions = AllPermissions;

    private static readonly PermissionType[] AuthorPermissions = [PermissionType.CreateCourse];

    private static readonly PermissionType[] DemoPermissions =
    [
        PermissionType.ReadPermission,
        PermissionType.ReadRole,
        PermissionType.ReadUser,
        PermissionType.CreateCourse,
        PermissionType.UpdateOwnProfile,
        PermissionType.UpdateOwnPassword,
    ];

    public static readonly Dictionary<DefaultRole, PermissionType[]> DefaultRolePermissions = new()
    {
        { DefaultRole.Admin, AdminPermissions },
        { DefaultRole.Author, AuthorPermissions },
        { DefaultRole.Demo, DemoPermissions },
    };
}
