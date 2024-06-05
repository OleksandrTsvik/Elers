using Application.Auth.GetInfo;
using Application.Users.DTOs;
using Application.Users.GetListUsers;
using Application.Users.GetUserById;

namespace Application.Common.Queries;

public interface IUserQueries
{
    Task<GetInfoResponse?> GetInfo(Guid id, CancellationToken cancellationToken = default);

    Task<GetListUserItemResponse[]> GetListUsers(CancellationToken cancellationToken = default);

    Task<GetUserByIdResponse?> GetUserById(Guid id, CancellationToken cancellationToken = default);

    Task<TeacherDto?> GetTeacherDtoById(Guid id, CancellationToken cancellationToken = default);
}
