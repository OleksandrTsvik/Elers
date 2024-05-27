using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Shared;

namespace Application.Common.Services;

public interface IImageService
{
    Task<Result<ImageUploadResult>> AddAsync(IFile image, CancellationToken cancellationToken = default);

    Task RemoveAsync(string imageName, CancellationToken cancellationToken = default);
}
