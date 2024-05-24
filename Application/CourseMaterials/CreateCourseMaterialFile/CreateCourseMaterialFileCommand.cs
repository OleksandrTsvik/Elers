using Application.Common.Interfaces;
using Application.Common.Messaging;

namespace Application.CourseMaterials.CreateCourseMaterialFile;

public record CreateCourseMaterialFileCommand(
    Guid TabId,
    string Title,
    IFile File) : ICommand;
