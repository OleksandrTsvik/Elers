using Application.Common.Interfaces;
using Application.Common.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.CourseMembers.EnrollToCourse;

public class EnrollToCourseCommandHandler : ICommandHandler<EnrollToCourseCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICourseMemberRepository _courseMemberRepository;
    private readonly ICourseRepository _courseRepository;
    private readonly IUserContext _userContext;

    public EnrollToCourseCommandHandler(
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

    public async Task<Result> Handle(EnrollToCourseCommand request, CancellationToken cancellationToken)
    {
        if (!await _courseRepository.ExistsByIdAsync(request.CourseId, cancellationToken))
        {
            return CourseErrors.NotFound(request.CourseId);
        }

        Guid userId = _userContext.UserId;

        if (await _courseMemberRepository.IsEnrolledAsync(request.CourseId, userId, cancellationToken))
        {
            return CourseMemberErrors.AlreadyEnrolled();
        }

        var courseMember = new CourseMember
        {
            CourseId = request.CourseId,
            UserId = userId
        };

        _courseMemberRepository.Add(courseMember);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
