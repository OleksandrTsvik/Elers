using FluentValidation;
using Infrastructure.CloudinarySetup;

namespace API.Options.Cloudinary;

public class CloudinarySettingsValidator : AbstractValidator<CloudinarySettings>
{
    public CloudinarySettingsValidator()
    {
        RuleFor(x => x.CloudName).NotEmpty();

        RuleFor(x => x.ApiKey).NotEmpty();

        RuleFor(x => x.ApiSecret).NotEmpty();
    }
}
