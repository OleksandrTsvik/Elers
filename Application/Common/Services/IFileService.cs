using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Shared;

namespace Application.Common.Services;

public interface IFileService
{
    Task<byte[]> DownloadAsync(string fileName, CancellationToken cancellationToken = default);

    Task<Result<FileUploadResult>> AddAsync(IFile file, CancellationToken cancellationToken = default);

    Task RemoveAsync(string fileName, CancellationToken cancellationToken = default);

    Task RemoveRangeAsync(IEnumerable<string> fileNames, CancellationToken cancellationToken = default);
}
