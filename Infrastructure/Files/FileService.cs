using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Services;
using Domain.Shared;

namespace Infrastructure.Files;

public class FileService : IFileService
{
    private const string FilesFolder = "files";
    private readonly IFileValidator _fileValidator;
    private readonly IFolderService _folderService;

    public FileService(IFileValidator fileValidator, IFolderService folderService)
    {
        _fileValidator = fileValidator;
        _folderService = folderService;
    }

    public Task<byte[]> DownloadAsync(string fileName, CancellationToken cancellationToken = default)
    {
        return File.ReadAllBytesAsync(GetFilePath(fileName), cancellationToken);
    }

    public async Task<Result<FileUploadResult>> AddAsync(
        IFile file,
        CancellationToken cancellationToken = default)
    {
        Result validationResult = _fileValidator.Validate(file);

        if (validationResult.IsFailure)
        {
            return validationResult.Error;
        }

        string fileName = file.FileName;
        string fileExtension = Path.GetExtension(fileName);
        string uniqueFileName = Guid.NewGuid().ToString() + fileExtension;

        string filePath = GetFilePath(uniqueFileName);

        using FileStream stream = File.Create(filePath);
        await file.CopyToAsync(stream, cancellationToken);

        return new FileUploadResult
        {
            FileName = fileName,
            UniqueFileName = uniqueFileName
        };
    }

    public Task RemoveAsync(string fileName, CancellationToken cancellationToken = default)
    {
        DeleteFile(fileName);

        return Task.CompletedTask;
    }

    public Task RemoveRangeAsync(
        IEnumerable<string> fileNames,
        CancellationToken cancellationToken = default)
    {
        foreach (string fileName in fileNames)
        {
            DeleteFile(fileName);
        }

        return Task.CompletedTask;
    }

    private string GetFilePath(string fileName)
    {
        return Path.Combine(_folderService.GetFolderPath(FilesFolder), fileName);
    }

    private void DeleteFile(string fileName)
    {
        string filePath = GetFilePath(fileName);

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }
}
