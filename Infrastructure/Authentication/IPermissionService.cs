namespace Infrastructure.Authentication;

public interface IPermissionService
{
    Task<List<string>> GetPermissionsAsync(Guid userId);
}
