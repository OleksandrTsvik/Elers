using Application.Common.Messaging;
using Application.Common.Queries;
using Application.CourseMaterials.DTOs;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.CourseMaterials.GetCourseMaterialContent;

public class GetCourseMaterialContentQueryHandler
    : IQueryHandler<GetCourseMaterialContentQuery, GetCourseMaterialContentResponse>
{
    private readonly ICourseMaterialQueries _courseMaterialQueries;
    private readonly ICourseMaterialRepository _courseMaterialRepository;

    public GetCourseMaterialContentQueryHandler(
        ICourseMaterialQueries courseMaterialQueries,
        ICourseMaterialRepository courseMaterialRepository)
    {
        _courseMaterialQueries = courseMaterialQueries;
        _courseMaterialRepository = courseMaterialRepository;
    }

    public async Task<Result<GetCourseMaterialContentResponse>> Handle(
        GetCourseMaterialContentQuery request,
        CancellationToken cancellationToken)
    {
        CourseMaterialTabResponseDto? courseMaterialTab = await _courseMaterialQueries
            .GetCourseMaterialTabResponseDto(request.TabId, cancellationToken);

        if (courseMaterialTab is null)
        {
            return CourseTabErrors.NotFound(request.TabId);
        }

        CourseMaterialContent? courseMaterial = await _courseMaterialRepository
            .GetByIdAsync<CourseMaterialContent>(request.Id, cancellationToken);

        if (courseMaterial is null)
        {
            return CourseMaterialErrors.NotFound(request.Id);
        }

        return new GetCourseMaterialContentResponse
        {
            Id = courseMaterial.Id,
            CourseId = courseMaterialTab.CourseId,
            TabId = courseMaterialTab.TabId,
            CourseTitle = courseMaterialTab.CourseTitle,
            CourseTabType = courseMaterialTab.CourseTabType,
            Content = courseMaterial.Content
        };
    }
}
