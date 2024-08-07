using Application.Common.Services;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Persistence.Constants;

namespace Persistence.Services;

public class CourseMemberPermissionService : ICourseMemberPermissionService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IMongoCollection<CourseMaterial> _courseMaterialsCollection;
    private readonly IMongoCollection<TestQuestion> _testQuestionsCollection;
    private readonly IMongoCollection<Grade> _gradesCollection;
    private readonly IMongoCollection<ManualGradesColumn> _manualGradesColumnsCollection;

    public CourseMemberPermissionService(ApplicationDbContext dbContext, IMongoDatabase mongoDatabase)
    {
        _dbContext = dbContext;

        _courseMaterialsCollection = mongoDatabase.GetCollection<CourseMaterial>(
            CollectionNames.CourseMaterials);

        _testQuestionsCollection = mongoDatabase.GetCollection<TestQuestion>(
            CollectionNames.TestQuestions);

        _gradesCollection = mongoDatabase.GetCollection<Grade>(CollectionNames.Grades);

        _manualGradesColumnsCollection = mongoDatabase.GetCollection<ManualGradesColumn>(
            CollectionNames.ManualGradesColumns);
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

    public async Task<string[]> GetCourseMemberPermissionsByCourseMemberIdAsync(
        Guid userId,
        Guid courseMemberId)
    {
        Guid courseId = await _dbContext.CourseMembers
            .Where(x => x.Id == courseMemberId)
            .Select(x => x.CourseId)
            .FirstOrDefaultAsync();

        if (courseId == Guid.Empty)
        {
            return [];
        }

        return await GetCourseMemberPermissionsByCourseIdAsync(userId, courseId);
    }

    public async Task<string[]> GetCourseMemberPermissionsByTestQuestionIdAsync(
        Guid userId,
        Guid testQuestionId)
    {
        Guid courseMaterialTestId = await _testQuestionsCollection
            .Find(x => x.Id == testQuestionId)
            .Project(x => x.TestId)
            .FirstOrDefaultAsync();

        if (courseMaterialTestId == Guid.Empty)
        {
            return [];
        }

        return await GetCourseMemberPermissionsByCourseMaterialIdAsync(userId, courseMaterialTestId);
    }

    public async Task<string[]> GetCourseMemberPermissionsByGradeIdAsync(Guid userId, Guid gradeId)
    {
        Guid courseId = await _gradesCollection
            .Find(x => x.Id == gradeId)
            .Project(x => x.CourseId)
            .FirstOrDefaultAsync();

        if (courseId == Guid.Empty)
        {
            return [];
        }

        return await GetCourseMemberPermissionsByCourseIdAsync(userId, courseId);
    }

    public async Task<string[]> GetCourseMemberPermissionsByColumnGradesIdAsync(Guid userId, Guid columnGradesId)
    {
        Guid courseId = await _manualGradesColumnsCollection
            .Find(x => x.Id == columnGradesId)
            .Project(x => x.CourseId)
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

    public Task<bool> IsCreatorByCourseMemberIdAsync(Guid userId, Guid courseMemberId)
    {
        return _dbContext.Courses.AnyAsync(x =>
            x.CreatorId == userId && x.CourseMembers.Any(courseMember =>
                courseMember.CourseId == x.Id && courseMember.Id == courseMemberId));
    }

    public async Task<bool> IsCreatorByTestQuestionIdAsync(Guid userId, Guid testQuestionId)
    {
        Guid courseMaterialTestId = await _testQuestionsCollection
            .Find(x => x.Id == testQuestionId)
            .Project(x => x.TestId)
            .FirstOrDefaultAsync();

        if (courseMaterialTestId == Guid.Empty)
        {
            return false;
        }

        return await IsCreatorByCourseMaterialIdAsync(userId, courseMaterialTestId);
    }

    public async Task<bool> IsCreatorByGradeIdAsync(Guid userId, Guid gradeId)
    {
        Guid courseId = await _gradesCollection
            .Find(x => x.Id == gradeId)
            .Project(x => x.CourseId)
            .FirstOrDefaultAsync();

        if (courseId == Guid.Empty)
        {
            return false;
        }

        return await IsCreatorByCourseIdAsync(userId, courseId);
    }

    public async Task<bool> IsCreatorByColumnGradesIdAsync(Guid userId, Guid columnGradesId)
    {
        Guid courseId = await _manualGradesColumnsCollection
            .Find(x => x.Id == columnGradesId)
            .Project(x => x.CourseId)
            .FirstOrDefaultAsync();

        if (courseId == Guid.Empty)
        {
            return false;
        }

        return await IsCreatorByCourseIdAsync(userId, courseId);
    }

    public async Task<bool> CheckCoursePermissionsAsync(
        Guid userId,
        Guid courseId,
        IEnumerable<CoursePermissionType> courseMemberPermissions,
        IEnumerable<PermissionType> userPermissions)
    {
        if (await IsCreatorByCourseIdAsync(userId, courseId))
        {
            return true;
        }

        if (courseMemberPermissions.Any() &&
            await _dbContext.CourseMembers.AnyAsync(x =>
                x.CourseId == courseId &&
                x.UserId == userId &&
                x.CourseRole != null &&
                x.CourseRole.CoursePermissions
                    .Any(coursePermission => courseMemberPermissions.Contains(coursePermission.Name))))
        {
            return true;
        }

        if (!userPermissions.Any())
        {
            return true;
        }

        return await _dbContext.Users
            .AnyAsync(user => user.Id == userId && user.Roles
                .Any(role => role.Permissions
                    .Any(permission => userPermissions.Contains(permission.Name))));
    }

    public async Task<bool> CheckCoursePermissionsByCourseTabIdAsync(
        Guid userId,
        Guid courseTabId,
        IEnumerable<CoursePermissionType> courseMemberPermissions,
        IEnumerable<PermissionType> userPermissions)
    {
        Guid courseId = await _dbContext.CourseTabs
            .Where(x => x.Id == courseTabId)
            .Select(x => x.CourseId)
            .FirstOrDefaultAsync();

        if (courseId == Guid.Empty)
        {
            return false;
        }

        return await CheckCoursePermissionsAsync(userId, courseId, courseMemberPermissions, userPermissions);
    }
}
