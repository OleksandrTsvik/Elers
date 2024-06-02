using Application.Common.Messaging;

namespace Application.CourseMembers.RemoveCourseMember;

public record RemoveCourseMemberCommand(Guid MemberId) : ICommand;
