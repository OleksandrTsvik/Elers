using Domain.Enums;
using Domain.Repositories;

namespace Infrastructure.Authentication;

public class PermissionService : IPermissionService
{
    private readonly IUserRepository _userRepository;

    public PermissionService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<string>> GetPermissionsAsync(Guid userId)
    {
        List<PermissionType> permissions = await _userRepository.GetPermissionsAsync(userId);

        return permissions.ConvertAll(x => x.ToString());
    }
}
