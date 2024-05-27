using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Services;
using CloudinaryDotNet;
using Domain.Shared;
using Infrastructure.Files;
using Microsoft.Extensions.Options;

namespace Infrastructure.CloudinarySetup;

public class CloudinaryImageService : IImageService
{
    private readonly Cloudinary _cloudinary;
    private readonly IFileValidator _fileValidator;

    public CloudinaryImageService(
        IOptions<CloudinarySettings> cloudinarySettings,
        IFileValidator fileValidator)
    {
        var account = new Account(
            cloudinarySettings.Value.CloudName,
            cloudinarySettings.Value.ApiKey,
            cloudinarySettings.Value.ApiSecret);

        _cloudinary = new Cloudinary(account);
        _fileValidator = fileValidator;
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

        using Stream stream = image.OpenReadStream();

        var uploadParams = new CloudinaryDotNet.Actions.ImageUploadParams
        {
            File = new FileDescription(image.FileName, stream),
        };

        CloudinaryDotNet.Actions.ImageUploadResult uploadResult = await _cloudinary.UploadAsync(
            uploadParams, cancellationToken);

        if (uploadResult.Error is not null)
        {
            throw new Exception(uploadResult.Error.Message);
        }

        return new ImageUploadResult
        {
            Url = uploadResult.Url.ToString(),
            UniqueName = uploadResult.PublicId
        };
    }

    public async Task RemoveAsync(string imageName, CancellationToken cancellationToken = default)
    {
        var deletionParams = new CloudinaryDotNet.Actions.DeletionParams(imageName);

        await _cloudinary.DestroyAsync(deletionParams);
    }
}
