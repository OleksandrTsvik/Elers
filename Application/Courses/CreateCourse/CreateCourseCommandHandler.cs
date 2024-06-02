using Application.Common.Interfaces;
using Application.Common.Messaging;
using Domain.Entities;
using Domain.Repositories;
using Domain.Shared;

namespace Application.Courses.CreateCourse;

public class CreateCourseCommandHandler : ICommandHandler<CreateCourseCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICourseRepository _courseRepository;
    private readonly IUserContext _userContext;

    public CreateCourseCommandHandler(
        IUnitOfWork unitOfWork,
        ICourseRepository courseRepository,
        IUserContext userContext)
    {
        _unitOfWork = unitOfWork;
        _courseRepository = courseRepository;
        _userContext = userContext;
    }

    public async Task<Result> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        var course = new Course
        {
            CreatorId = _userContext.UserId,
            Title = request.Title,
            Description = request.Description
        };

        if (request.TabType.HasValue)
        {
            course.TabType = request.TabType.Value;
        }

        _courseRepository.Add(course);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
