using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Services;
using Domain.Shared;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Files;

public class ImageService : IImageService
{
    private const string ImagesFolder = "images";
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IFileValidator _fileValidator;
    private readonly IFolderService _folderService;

    public ImageService(
        IHttpContextAccessor httpContextAccessor,
        IFileValidator fileValidator,
        IFolderService folderService)
    {
        _httpContextAccessor = httpContextAccessor;
        _fileValidator = fileValidator;
        _folderService = folderService;
    }

    public async Task<Result<ImageUploadResult>> AddAsync(
        IFile image,
        CancellationToken cancellationToken = default)
    {
        Result validationResult = _fileValidator.ValidateImage(image);

        if (validationResult.IsFailure)
        {
            return validationResult.Error;
        }

        string imageExtension = Path.GetExtension(image.FileName);
        string uniqueImageName = Guid.NewGuid().ToString() + imageExtension;
        string imagePath = GetImagePath(uniqueImageName);

        using FileStream stream = File.Create(imagePath);
        await image.CopyToAsync(stream, cancellationToken);

        return new ImageUploadResult
        {
            Url = GetImageUrl(uniqueImageName),
            UniqueName = uniqueImageName
        };
    }

    public Task RemoveAsync(string imageName, CancellationToken cancellationToken = default)
    {
        string imagePath = GetImagePath(imageName);

        if (File.Exists(imagePath))
        {
            File.Delete(imagePath);
        }

        return Task.CompletedTask;
    }

    private string GetImagePath(string imageName)
    {
        return Path.Combine(_folderService.GetFolderPath(ImagesFolder), imageName);
    }

    private string GetImageUrl(string imageName)
    {
        HttpRequest? request = _httpContextAccessor.HttpContext?.Request;

        if (request is null)
        {
            return $"/api/images/{imageName}";
        }

        return $"{request.Scheme}://{request.Host}{request.PathBase}/api/images/{imageName}";
    }
}
