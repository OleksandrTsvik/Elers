using Application.Common.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.CourseMaterials.UpdateCourseMaterialTest;

public class UpdateCourseMaterialTestCommandHandler : ICommandHandler<UpdateCourseMaterialTestCommand>
{
    private readonly ICourseMaterialRepository _courseMaterialRepository;
    private readonly IGradeRepository _gradeRepository;

    public UpdateCourseMaterialTestCommandHandler(
        ICourseMaterialRepository courseMaterialRepository,
        IGradeRepository gradeRepository)
    {
        _courseMaterialRepository = courseMaterialRepository;
        _gradeRepository = gradeRepository;
    }

    public async Task<Result> Handle(
        UpdateCourseMaterialTestCommand request,
        CancellationToken cancellationToken)
    {
        CourseMaterialTest? courseMaterialTest = await _courseMaterialRepository
            .GetByIdAsync<CourseMaterialTest>(request.MaterialId, cancellationToken);

        if (courseMaterialTest is null)
        {
            return CourseMaterialErrors.NotFound(request.MaterialId);
        }

        if (courseMaterialTest.GradingMethod != request.GradingMethod)
        {
            await _gradeRepository.UpdateTestGradingMethodAsync(
                courseMaterialTest.Id, request.GradingMethod, cancellationToken);
        }

        courseMaterialTest.Title = request.Title;
        courseMaterialTest.Description = request.Description;
        courseMaterialTest.NumberAttempts = request.NumberAttempts;
        courseMaterialTest.TimeLimitInMinutes = request.TimeLimitInMinutes;
        courseMaterialTest.Deadline = request.Deadline;
        courseMaterialTest.GradingMethod = request.GradingMethod;
        courseMaterialTest.ShuffleQuestions = request.ShuffleQuestions;

        await _courseMaterialRepository.UpdateAsync(courseMaterialTest, cancellationToken);

        return Result.Success();
    }
}
