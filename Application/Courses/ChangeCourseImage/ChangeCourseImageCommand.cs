using Application.Common.Interfaces;
using Application.Common.Messaging;

namespace Application.Courses.ChangeCourseImage;

public record ChangeCourseImageCommand(Guid Id, IFile Image) : ICommand;
