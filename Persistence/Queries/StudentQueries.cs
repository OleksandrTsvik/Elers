using Application.Common.Queries;
using Application.Users.DTOs;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Queries;

public class StudentQueries : IStudentQueries
{
    private readonly ApplicationDbContext _dbContext;

    public StudentQueries(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<UserDto[]> GetCourseStudents(Guid courseId, CancellationToken cancellationToken = default)
    {
        return _dbContext.CourseMembers
            .Where(x => x.CourseId == courseId && x.User != null && x.User.Type == UserType.Student)
            .OrderBy(x => x.User!.LastName)
            .Select(x => new UserDto
            {
                Id = x.User!.Id,
                FirstName = x.User.FirstName,
                LastName = x.User.LastName,
                Patronymic = x.User.Patronymic,
                AvatarUrl = x.User.AvatarUrl
            })
            .ToArrayAsync(cancellationToken);
    }
}
