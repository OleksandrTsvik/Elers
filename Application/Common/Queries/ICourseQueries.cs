using Application.Courses.GetCourseById;
using Application.Courses.GetCourseByIdToEdit;
using Application.Courses.GetCourseByTabId;
using Application.Courses.GetListCourses;

namespace Application.Common.Queries;

public interface ICourseQueries
{
    Task<GetCourseByIdResponseDto?> GetCourseById(Guid id, CancellationToken cancellationToken = default);

    Task<GetCourseByIdToEditResponseDto?> GetCourseByIdToEdit(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<GetCourseByTabIdResponse?> GetCourseByTabId(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<GetListCourseItemResponse[]> GetListCourses(CancellationToken cancellationToken = default);
}
