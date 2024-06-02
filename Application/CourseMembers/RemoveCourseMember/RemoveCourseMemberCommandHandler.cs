using Application.Common.Interfaces;
using Application.Common.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.CourseMembers.RemoveCourseMember;

public class RemoveCourseMemberCommandHandler : ICommandHandler<RemoveCourseMemberCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICourseMemberRepository _courseMemberRepository;

    public RemoveCourseMemberCommandHandler(
        IUnitOfWork unitOfWork,
        ICourseMemberRepository courseMemberRepository)
    {
        _unitOfWork = unitOfWork;
        _courseMemberRepository = courseMemberRepository;
    }

    public async Task<Result> Handle(RemoveCourseMemberCommand request, CancellationToken cancellationToken)
    {
        CourseMember? courseMember = await _courseMemberRepository.GetByIdAsync(
            request.MemberId, cancellationToken);

        if (courseMember is null)
        {
            return CourseMemberErrors.NotFound();
        }

        _courseMemberRepository.Remove(courseMember);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
