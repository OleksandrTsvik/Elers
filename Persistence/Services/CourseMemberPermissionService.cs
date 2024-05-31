using Application.Common.Services;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Persistence.Constants;

namespace Persistence.Services;

public class CourseMemberPermissionService : ICourseMemberPermissionService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IMongoCollection<CourseMaterial> _courseMaterialsCollection;

    public CourseMemberPermissionService(ApplicationDbContext dbContext, IMongoDatabase mongoDatabase)
    {
        _dbContext = dbContext;

        _courseMaterialsCollection = mongoDatabase.GetCollection<CourseMaterial>(
            CollectionNames.CourseMaterials);
    }

    public Task<string[]> GetCourseMemberPermissionsByCourseIdAsync(Guid userId, Guid courseId)
    {
        return _dbContext.CourseMembers
            .AsSplitQuery()
            .Where(courseMember =>
                courseMember.UserId == userId &&
                courseMember.CourseId == courseId &&
                courseMember.CourseRole != null)
            .Select(courseMember => courseMember.CourseRole!.CoursePermissions)
            .SelectMany(coursePermissions => coursePermissions)
            .Select(coursePermission => coursePermission.Name.ToString())
            .ToArrayAsync();
    }

    public async Task<string[]> GetCourseMemberPermissionsByCourseTabIdAsync(Guid userId, Guid courseTabId)
    {
        Guid courseId = await _dbContext.CourseTabs
            .Where(x => x.Id == courseTabId)
            .Select(x => x.CourseId)
            .FirstOrDefaultAsync();

        if (courseId == Guid.Empty)
        {
            return [];
        }

        return await GetCourseMemberPermissionsByCourseIdAsync(userId, courseId);
    }

    public async Task<string[]> GetCourseMemberPermissionsByCourseMaterialIdAsync(
        Guid userId,
        Guid courseMaterialId)
    {
        Guid courseTabId = await _courseMaterialsCollection
            .Find(x => x.Id == courseMaterialId)
            .Project(x => x.CourseTabId)
            .FirstOrDefaultAsync();

        if (courseTabId == Guid.Empty)
        {
            return [];
        }

        return await GetCourseMemberPermissionsByCourseTabIdAsync(userId, courseTabId);
    }

    public async Task<string[]> GetCourseMemberPermissionsByCourseRoleIdAsync(Guid userId, Guid courseRoleId)
    {
        Guid courseId = await _dbContext.CourseRoles
            .Where(x => x.Id == courseRoleId)
            .Select(x => x.CourseId)
            .FirstOrDefaultAsync();

        if (courseId == Guid.Empty)
        {
            return [];
        }

        return await GetCourseMemberPermissionsByCourseIdAsync(userId, courseId);
    }

    public Task<bool> IsCreatorByCourseIdAsync(Guid userId, Guid courseId)
    {
        return _dbContext.Courses.AnyAsync(x => x.CreatorId == userId && x.Id == courseId);
    }

    public Task<bool> IsCreatorByCourseTabIdAsync(Guid userId, Guid courseTabId)
    {
        return _dbContext.Courses.AnyAsync(x =>
            x.CreatorId == userId &&
            x.CourseTabs.Any(courseTab => courseTab.CourseId == x.Id && courseTab.Id == courseTabId));
    }

    public async Task<bool> IsCreatorByCourseMaterialIdAsync(Guid userId, Guid courseMaterialId)
    {
        Guid courseTabId = await _courseMaterialsCollection
            .Find(x => x.Id == courseMaterialId)
            .Project(x => x.CourseTabId)
            .FirstOrDefaultAsync();

        if (courseTabId == Guid.Empty)
        {
            return false;
        }

        return await IsCreatorByCourseTabIdAsync(userId, courseTabId);
    }

    public Task<bool> IsCreatorByCourseRoleIdAsync(Guid userId, Guid courseRoleId)
    {
        return _dbContext.Courses.AnyAsync(x =>
            x.CreatorId == userId &&
            x.CourseRoles.Any(courseRole => courseRole.CourseId == x.Id && courseRole.Id == courseRoleId));
    }
}
