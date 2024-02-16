using Domain.Enums;
using Microsoft.AspNetCore.Authorization;

namespace Infrastructure.Authentication;

public class HasRolesAttribute : AuthorizeAttribute
{
    public HasRolesAttribute(params Roles[] roles)
    {
        Roles = string.Join(",", roles);
    }
}
