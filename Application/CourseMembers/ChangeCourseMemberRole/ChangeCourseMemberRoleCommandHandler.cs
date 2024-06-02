using Application.Common.Interfaces;
using Application.Common.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.CourseMembers.ChangeCourseMemberRole;

public class ChangeCourseMemberRoleCommandHandler : ICommandHandler<ChangeCourseMemberRoleCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICourseMemberRepository _courseMemberRepository;
    private readonly ICourseRoleRepository _courseRoleRepository;

    public ChangeCourseMemberRoleCommandHandler(
        IUnitOfWork unitOfWork,
        ICourseMemberRepository courseMemberRepository,
        ICourseRoleRepository courseRoleRepository)
    {
        _unitOfWork = unitOfWork;
        _courseMemberRepository = courseMemberRepository;
        _courseRoleRepository = courseRoleRepository;
    }

    public async Task<Result> Handle(
        ChangeCourseMemberRoleCommand request,
        CancellationToken cancellationToken)
    {
        CourseMember? courseMember = await _courseMemberRepository.GetByIdAsync(
            request.MemberId, cancellationToken);

        if (courseMember is null)
        {
            return CourseMemberErrors.NotFound();
        }

        if (request.CourseRoleId.HasValue)
        {
            CourseRole? courseRole = await _courseRoleRepository.GetByIdAsync(
                request.CourseRoleId.Value, cancellationToken);

            if (courseRole is null)
            {
                return CourseRoleErrors.NotFound(request.CourseRoleId.Value);
            }

            if (courseMember.CourseId != courseRole.CourseId)
            {
                return CourseMemberErrors.RoleFromAnotherCourse();
            }
        }

        courseMember.CourseRoleId = request.CourseRoleId;

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
