using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Shared;

namespace Application.Common.Services;

public interface IFileService
{
    Task<Result<FileUploadResult>> AddAsync(IFile file);

    Task RemoveAsync(string uniqueFileName);

    Task RemoveRangeAsync(IEnumerable<string> uniqueFileNames);
}
