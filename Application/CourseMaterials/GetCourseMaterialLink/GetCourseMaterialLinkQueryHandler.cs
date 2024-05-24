using Application.Common.Messaging;
using Application.Common.Queries;
using Application.CourseMaterials.DTOs;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.CourseMaterials.GetCourseMaterialLink;

public class GetCourseMaterialLinkQueryHandler
    : IQueryHandler<GetCourseMaterialLinkQuery, GetCourseMaterialLinkResponse>
{
    private readonly ICourseMaterialQueries _courseMaterialQueries;
    private readonly ICourseMaterialRepository _courseMaterialRepository;

    public GetCourseMaterialLinkQueryHandler(
        ICourseMaterialQueries courseMaterialQueries,
        ICourseMaterialRepository courseMaterialRepository)
    {
        _courseMaterialQueries = courseMaterialQueries;
        _courseMaterialRepository = courseMaterialRepository;
    }

    public async Task<Result<GetCourseMaterialLinkResponse>> Handle(
        GetCourseMaterialLinkQuery request,
        CancellationToken cancellationToken)
    {
        CourseMaterialTabResponseDto? courseMaterialTab = await _courseMaterialQueries
            .GetCourseMaterialTabResponseDto(request.TabId, cancellationToken);

        if (courseMaterialTab is null)
        {
            return CourseTabErrors.NotFound(request.TabId);
        }

        CourseMaterialLink? courseMaterial = await _courseMaterialRepository
            .GetByIdAsync<CourseMaterialLink>(request.Id, cancellationToken);

        if (courseMaterial is null)
        {
            return CourseMaterialErrors.NotFound(request.Id);
        }

        return new GetCourseMaterialLinkResponse
        {
            Id = courseMaterial.Id,
            CourseId = courseMaterialTab.CourseId,
            TabId = courseMaterialTab.TabId,
            CourseTitle = courseMaterialTab.CourseTitle,
            CourseTabType = courseMaterialTab.CourseTabType,
            Title = courseMaterial.Title,
            Link = courseMaterial.Link
        };
    }
}
