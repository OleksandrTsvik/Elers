namespace Infrastructure.Authentication;

public interface IRolesService
{
    Task<HashSet<string>> GetRolesAsync(Guid userId);
}
