using Application.Common.Interfaces;
using Domain.Errors;
using Domain.Shared;
using Microsoft.Extensions.Options;

namespace Infrastructure.Files;

public class FileValidator : IFileValidator
{
    private readonly int _fileSizeLimit;

    public FileValidator(IOptions<FileSettingsOptions> fileSettingsOptions)
    {
        _fileSizeLimit = fileSettingsOptions.Value.SizeLimit;
    }

    public Result Validate(IFile file)
    {
        if (file.Length <= 0)
        {
            return FileErrors.Empty(file.FileName);
        }

        if (file.Length > _fileSizeLimit)
        {
            return FileErrors.SizeLimit(file.FileName);
        }

        return Result.Success();
    }
}
