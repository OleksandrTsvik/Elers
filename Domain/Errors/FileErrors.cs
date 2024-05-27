using Domain.Constants;
using Domain.Shared;

namespace Domain.Errors;

public static class FileErrors
{
    public static Error Empty(string fileName) => Error.Conflict(
        ErrorCodes.Files.Empty,
        $"File with name = '{fileName}' is empty.", fileName);

    public static Error SizeLimit(string fileName) => Error.Conflict(
        ErrorCodes.Files.SizeLimit,
        $"The file with name = '{fileName}' exceeds the maximum allowed size.", fileName);

    public static Error InvalidImage(string fileName) => Error.Conflict(
        ErrorCodes.Files.InvalidImage,
        $"The file with name = '{fileName}' is not an image.", fileName);
}
