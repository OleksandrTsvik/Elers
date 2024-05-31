using Domain.Entities;

namespace Domain.Repositories;

public interface ICourseMemberRepository
{
    Task<CourseMember?> GetByCourseIdAndUserIdAsync(
        Guid courseId,
        Guid userId,
        CancellationToken cancellationToken = default);

    void Add(CourseMember courseMember);

    void Update(CourseMember courseMember);

    void Remove(CourseMember courseMember);

    Task<bool> IsEnrolledAsync(Guid courseId, Guid userId, CancellationToken cancellationToken = default);
}
