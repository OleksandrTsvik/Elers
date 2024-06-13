using Application.Common.Messaging;

namespace Application.Profile.GetMyEnrolledCourses;

public record GetMyEnrolledCoursesQuery() : IQuery<GetMyEnrolledCoursesResponse[]>;
