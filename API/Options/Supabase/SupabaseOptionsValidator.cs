using FluentValidation;
using Infrastructure.SupabaseSetup;

namespace API.Options.Supabase;

public class SupabaseSettingsValidator : AbstractValidator<SupabaseSettings>
{
    public SupabaseSettingsValidator()
    {
        RuleFor(x => x.Url).NotEmpty();

        RuleFor(x => x.Key).NotEmpty();

        RuleFor(x => x.BucketName).NotEmpty();
    }
}
