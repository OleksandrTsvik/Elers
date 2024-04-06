using Domain.Enums;
using Microsoft.AspNetCore.Authorization;

namespace Infrastructure.Authentication;

public class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(PermissionType permission)
        : base(policy: permission.ToString())
    {
    }
}
