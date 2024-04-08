using Domain.Enums;

namespace Domain.Constants;

public static class PermissionsSetup
{
    public static readonly PermissionType[] AllPermissions = Enum.GetValues<PermissionType>();

    private static readonly PermissionType[] DefaultAdminPermissions = AllPermissions;

    private static readonly PermissionType[] DefaultTeacherPermissions = [];

    private static readonly PermissionType[] DefaultStudentPermissions = [];

    public static readonly Dictionary<DefaultRole, PermissionType[]> DefaultRolePermissions = new()
    {
        { DefaultRole.Admin, DefaultAdminPermissions },
        { DefaultRole.Teacher, DefaultTeacherPermissions },
        { DefaultRole.Student, DefaultStudentPermissions }
    };

    public static bool ContainsPermission(this PermissionType[] permissions, string permission)
        => permissions.Any(x => x.ToString() == permission);
}