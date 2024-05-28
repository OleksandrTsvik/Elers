using Application.Common.Messaging;
using Application.Common.Queries;
using Application.CourseMaterials.DTOs;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.CourseMaterials.GetCourseMaterialFile;

public class GetCourseMaterialFileQueryHandler
    : IQueryHandler<GetCourseMaterialFileQuery, GetCourseMaterialFileResponse>
{
    private readonly ICourseMaterialQueries _courseMaterialQueries;
    private readonly ICourseMaterialRepository _courseMaterialRepository;

    public GetCourseMaterialFileQueryHandler(
        ICourseMaterialQueries courseMaterialQueries,
        ICourseMaterialRepository courseMaterialRepository)
    {
        _courseMaterialQueries = courseMaterialQueries;
        _courseMaterialRepository = courseMaterialRepository;
    }

    public async Task<Result<GetCourseMaterialFileResponse>> Handle(
        GetCourseMaterialFileQuery request,
        CancellationToken cancellationToken)
    {
        CourseMaterialTabResponseDto? courseMaterialTab = await _courseMaterialQueries
            .GetCourseMaterialTabResponseDto(request.TabId, cancellationToken);

        if (courseMaterialTab is null)
        {
            return CourseTabErrors.NotFound(request.TabId);
        }

        CourseMaterialFile? courseMaterial = await _courseMaterialRepository
            .GetByIdAsync<CourseMaterialFile>(request.Id, cancellationToken);

        if (courseMaterial is null)
        {
            return CourseMaterialErrors.NotFound(request.Id);
        }

        return new GetCourseMaterialFileResponse
        {
            Id = courseMaterial.Id,
            CourseId = courseMaterialTab.CourseId,
            TabId = courseMaterialTab.TabId,
            CourseTitle = courseMaterialTab.CourseTitle,
            CourseTabType = courseMaterialTab.CourseTabType,
            FileTitle = courseMaterial.Title
        };
    }
}
