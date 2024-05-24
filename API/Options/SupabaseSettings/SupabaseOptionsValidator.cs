using FluentValidation;
using Infrastructure.SupabaseSetup;

namespace API.Options.Supabase;

public class SupabaseOptionsValidator : AbstractValidator<SupabaseSettingsOptions>
{
    public SupabaseOptionsValidator()
    {
        RuleFor(x => x.Url).NotEmpty();

        RuleFor(x => x.Key).NotEmpty();

        RuleFor(x => x.BucketName).NotEmpty();
    }
}
