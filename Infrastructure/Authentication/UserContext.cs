using System.Security.Claims;
using Application.Common.Interfaces;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Authentication;

public class UserContext : IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid UserId
    {
        get
        {
            string? userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

            return Guid.TryParse(userId, out Guid parsedUserId)
                ? parsedUserId
                : throw new UserIdUnavailableException();
        }
    }

    public bool IsAuthenticated => _httpContextAccessor
        .HttpContext?.User?.Identity?.IsAuthenticated ?? false;
}
