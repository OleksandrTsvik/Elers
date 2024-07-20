using FluentValidation;
using Infrastructure.Files;

namespace Api.Options.File;

public class FileSettingsValidator : AbstractValidator<FileSettings>
{
    public FileSettingsValidator()
    {
        RuleFor(x => x.SizeLimit).GreaterThan(0);

        RuleFor(x => x.ImageSizeLimit).GreaterThan(0);
    }
}
