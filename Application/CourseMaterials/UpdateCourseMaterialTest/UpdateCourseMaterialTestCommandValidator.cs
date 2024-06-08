using Application.Common.Extensions;
using Application.Common.Interfaces;
using Domain.Rules;
using FluentValidation;

namespace Application.CourseMaterials.UpdateCourseMaterialTest;

public class UpdateCourseMaterialTestCommandValidator : AbstractValidator<UpdateCourseMaterialTestCommand>
{
    public UpdateCourseMaterialTestCommandValidator(ITranslator translator)
    {
        RuleFor(x => x.MaterialId).NotEmpty();

        RuleFor(x => x.Title)
            .NotEmpty()
            .MinimumLength(CourseMaterialRules.MinTitleTestLength)
            .MaximumLength(CourseMaterialRules.MaxTitleTestLength)
            .TrimWhitespace(translator);

        RuleFor(x => x.NumberAttempts).GreaterThanOrEqualTo(1);

        RuleFor(x => x.TimeLimitInMinutes)
            .GreaterThanOrEqualTo(1)
                .When(x => x.TimeLimitInMinutes is not null);
    }
}
