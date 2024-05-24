using Application.Common.Extensions;
using Application.Common.Interfaces;
using Domain.Rules;
using FluentValidation;

namespace Application.CourseMaterials.CreateCourseMaterialLink;

public class CreateCourseMaterialLinkCommandValidator : AbstractValidator<CreateCourseMaterialLinkCommand>
{
    public CreateCourseMaterialLinkCommandValidator(ITranslator translator)
    {
        RuleFor(x => x.TabId).NotEmpty();

        RuleFor(x => x.Title)
            .NotEmpty()
            .MinimumLength(CourseMaterialRules.MinTitleLinkLength)
            .MaximumLength(CourseMaterialRules.MaxTitleLinkLength)
            .TrimWhitespace(translator);

        RuleFor(x => x.Link)
            .NotEmpty()
            .MaximumLength(CourseMaterialRules.MaxLinkLength)
            .IsUrl(translator)
            .TrimWhitespace(translator);
    }
}
