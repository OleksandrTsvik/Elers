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

    public CreateCourseCommandHandler(IUnitOfWork unitOfWork, ICourseRepository courseRepository)
    {
        _unitOfWork = unitOfWork;
        _courseRepository = courseRepository;
    }

    public async Task<Result> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        var course = new Course
        {
            Title = request.Title,
            Description = request.Description,
            TabType = request.TabType
        };

        _courseRepository.Add(course);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
