using Application.Common.Interfaces;
using Application.Common.Messaging;

namespace Application.CourseMaterials.UpdateCourseMaterialFile;

public record UpdateCourseMaterialFileCommand(
    Guid Id,
    string Title,
    IFile? File) : ICommand;
