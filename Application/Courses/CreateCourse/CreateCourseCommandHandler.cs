using Application.Common.Interfaces;
using Application.Common.Messaging;
using Domain.Entities;
using Domain.Shared;

namespace Application.Courses.CreateCourse;

public class CreateCourseCommandHandler : ICommandHandler<CreateCourseCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateCourseCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        var course = new Course
        {
            Title = request.Title,
            Description = request.Description
        };

        _context.Courses.Add(course);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
