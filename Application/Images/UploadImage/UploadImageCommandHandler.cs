using Application.Common.Messaging;
using Application.Common.Models;
using Application.Common.Services;
using Domain.Shared;

namespace Application.Images.UploadImage;

public class UploadImageCommandHandler : ICommandHandler<UploadImageCommand, UploadImageResponse>
{
    private readonly IImageService _imageService;

    public UploadImageCommandHandler(IImageService imageService)
    {
        _imageService = imageService;
    }

    public async Task<Result<UploadImageResponse>> Handle(
        UploadImageCommand request,
        CancellationToken cancellationToken)
    {
        Result<ImageUploadResult> addImageResult = await _imageService.AddAsync(
            request.Image, cancellationToken);

        if (addImageResult.IsFailure || addImageResult.Value is null)
        {
            return addImageResult.Error;
        }

        return new UploadImageResponse
        {
            Url = addImageResult.Value.Url
        };
    }
}
