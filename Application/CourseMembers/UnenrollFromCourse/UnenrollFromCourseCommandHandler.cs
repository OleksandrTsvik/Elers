using Application.Common.Interfaces;
using Application.Common.Messaging;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.CourseMembers.UnenrollFromCourse;

public class UnenrollFromCourseCommandHandler : ICommandHandler<UnenrollFromCourseCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICourseMemberRepository _courseMemberRepository;
    private readonly ICourseRepository _courseRepository;
    private readonly IUserContext _userContext;

    public UnenrollFromCourseCommandHandler(
        IUnitOfWork unitOfWork,
        ICourseMemberRepository courseMemberRepository,
        ICourseRepository courseRepository,
        IUserContext userContext)
    {
        _unitOfWork = unitOfWork;
        _courseMemberRepository = courseMemberRepository;
        _courseRepository = courseRepository;
        _userContext = userContext;
    }

    public async Task<Result> Handle(UnenrollFromCourseCommand request, CancellationToken cancellationToken)
    {
        if (!await _courseRepository.ExistsByIdAsync(request.CourseId, cancellationToken))
        {
            return CourseErrors.NotFound(request.CourseId);
        }

        var courseMember = await _courseMemberRepository.GetByCourseIdAndUserIdAsync(
            request.CourseId, _userContext.UserId, cancellationToken);

        if (courseMember is null)
        {
            return CourseMemberErrors.NotEnrolled();
        }

        _courseMemberRepository.Remove(courseMember);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
