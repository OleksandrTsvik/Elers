using Application.Common.Interfaces;
using Application.Common.Messaging;

namespace Application.Images.UploadImage;

public record UploadImageCommand(IFile Image) : ICommand<UploadImageResponse>;
