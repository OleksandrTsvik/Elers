using Application.Common.Messaging;

namespace Application.CourseMembers.ChangeCourseMemberRole;

public record ChangeCourseMemberRoleCommand(
    Guid MemberId,
    Guid? CourseRoleId) : ICommand;
