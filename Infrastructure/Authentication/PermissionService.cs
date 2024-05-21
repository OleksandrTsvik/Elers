using Domain.Repositories;

namespace Infrastructure.Authentication;

public class PermissionService : IPermissionService
{
    private readonly IUserRepository _userRepository;

    public PermissionService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public Task<List<string>> GetPermissionsAsync(Guid userId)
    {
        return _userRepository.GetPermissionsAsync(userId);
    }
}
