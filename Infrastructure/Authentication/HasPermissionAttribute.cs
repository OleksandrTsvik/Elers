using Domain.Enums;
using Microsoft.AspNetCore.Authorization;

namespace Infrastructure.Authentication;

public class HasPermissionAttribute : AuthorizeAttribute
{
    public const string PolicyPrefix = "PERMISSION:";

    public HasPermissionAttribute(params PermissionType[] permissions)
    {
        Policy = $"{PolicyPrefix}{string.Join(",", permissions)}";
    }
}
