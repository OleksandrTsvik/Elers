using Application.Common.Interfaces;
using Domain.Errors;
using Domain.Shared;
using Microsoft.Extensions.Options;

namespace Infrastructure.Files;

public class FileValidator : IFileValidator
{
    private readonly int _fileSizeLimit;
    private readonly int _imageSizeLimit;

    public FileValidator(IOptions<FileSettings> fileSettingsOptions)
    {
        _fileSizeLimit = fileSettingsOptions.Value.SizeLimit;
        _imageSizeLimit = fileSettingsOptions.Value.ImageSizeLimit;
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

    public Result ValidateImage(IFile image)
    {
        if (image.Length <= 0)
        {
            return FileErrors.Empty(image.FileName);
        }

        if (image.Length > _imageSizeLimit)
        {
            return FileErrors.SizeLimit(image.FileName);
        }

        if (!IsImage(image))
        {
            return FileErrors.InvalidImage(image.FileName);
        }

        return Result.Success();
    }

    private static bool IsImage(IFile image)
    {
        if (!string.IsNullOrEmpty(image.ContentType) &&
            !image.ContentType.StartsWith("image/", StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        string[] imageExtensions =
        [
            ".jpg", ".jpeg", ".png",
            ".svg", ".webp", ".bmp",
            ".tif", ".tiff", ".ico",
            ".gif"
        ];

        string imageExtension = Path.GetExtension(image.FileName).ToLower();

        if (!string.IsNullOrEmpty(imageExtension) &&
            !imageExtensions.Contains(imageExtension))
        {
            return false;
        }

        return true;
    }
}
