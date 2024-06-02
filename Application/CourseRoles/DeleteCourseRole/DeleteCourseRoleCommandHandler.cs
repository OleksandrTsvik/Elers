using Application.Common.Interfaces;
using Application.Common.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.CourseRoles.DeleteCourseRole;

public class DeleteCourseRoleCommandHandler : ICommandHandler<DeleteCourseRoleCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICourseRoleRepository _courseRoleRepository;

    public DeleteCourseRoleCommandHandler(IUnitOfWork unitOfWork, ICourseRoleRepository courseRoleRepository)
    {
        _unitOfWork = unitOfWork;
        _courseRoleRepository = courseRoleRepository;
    }

    public async Task<Result> Handle(DeleteCourseRoleCommand request, CancellationToken cancellationToken)
    {
        CourseRole? courseRole = await _courseRoleRepository.GetByIdAsync(request.RoleId, cancellationToken);

        if (courseRole is null)
        {
            return CourseRoleErrors.NotFound(request.RoleId);
        }

        _courseRoleRepository.Remove(courseRole);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
