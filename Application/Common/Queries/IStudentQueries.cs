using Application.Users.DTOs;

namespace Application.Common.Queries;

public interface IStudentQueries
{
    Task<UserDto[]> GetCourseStudents(Guid courseId, CancellationToken cancellationToken = default);
}
