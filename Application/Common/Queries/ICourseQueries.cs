using Application.Common.Models;
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

    Task<PagedList<GetListCourseItemResponse>> GetListCourses(
        GetListCoursesQueryParams queryParams,
        CancellationToken cancellationToken = default);
}
