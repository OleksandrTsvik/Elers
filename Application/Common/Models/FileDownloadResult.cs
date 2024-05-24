namespace Application.Common.Models;

public class FileDownloadResult
{
    public required string FileName { get; init; }
    public required byte[] FileContents { get; init; }
}
