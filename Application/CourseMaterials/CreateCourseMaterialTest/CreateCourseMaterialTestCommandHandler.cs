using Application.Common.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.CourseMaterials.CreateCourseMaterialTest;

public class CreateCourseMaterialTestCommandHandler : ICommandHandler<CreateCourseMaterialTestCommand, Guid>
{
    private readonly ICourseMaterialRepository _courseMaterialRepository;
    private readonly ICourseTabRepository _courseTabRepository;

    public CreateCourseMaterialTestCommandHandler(
        ICourseMaterialRepository courseMaterialRepository,
        ICourseTabRepository courseTabRepository)
    {
        _courseMaterialRepository = courseMaterialRepository;
        _courseTabRepository = courseTabRepository;
    }

    public async Task<Result<Guid>> Handle(
        CreateCourseMaterialTestCommand request,
        CancellationToken cancellationToken)
    {
        if (!await _courseTabRepository.ExistsByIdAsync(request.CourseTabId, cancellationToken))
        {
            return CourseTabErrors.NotFound(request.CourseTabId);
        }

        var courseMaterialTest = new CourseMaterialTest
        {
            CourseTabId = request.CourseTabId,
            Title = request.Title,
            Description = request.Description,
            NumberAttempts = request.NumberAttempts,
            TimeLimitInMinutes = request.TimeLimitInMinutes,
            Deadline = request.Deadline,
            GradingMethod = request.GradingMethod
        };

        await _courseMaterialRepository.AddAsync(courseMaterialTest, cancellationToken);

        return courseMaterialTest.Id;
    }
}
