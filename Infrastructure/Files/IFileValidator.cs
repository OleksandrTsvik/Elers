using Application.Common.Interfaces;
using Domain.Shared;

namespace Infrastructure.Files;

public interface IFileValidator
{
    Result Validate(IFile file);

    Result ValidateImage(IFile image);
}
