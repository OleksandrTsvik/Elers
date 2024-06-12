using Application.Common.Models;
using Domain.Enums;

namespace Application.Users.GetListUsers;

public class GetListUsersQueryParams : PagingParams
{
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string? Patronymic { get; init; }
    public string? Email { get; init; }
    public Guid[]? Roles { get; set; }
    public UserType[]? Types { get; init; }
    public string? SortColumn { get; set; }
    public SortOrder? SortOrder { get; set; }
}
