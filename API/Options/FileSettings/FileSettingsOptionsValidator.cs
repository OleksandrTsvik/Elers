using FluentValidation;
using Infrastructure.Files;

namespace API.Options.FileSettings;

public class FileSettingsOptionsValidator : AbstractValidator<FileSettingsOptions>
{
    public FileSettingsOptionsValidator()
    {
        RuleFor(x => x.SizeLimit).GreaterThan(0);
    }
}
