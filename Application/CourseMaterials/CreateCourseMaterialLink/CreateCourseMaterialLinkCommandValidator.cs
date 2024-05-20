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
            .MaximumLength(CourseMaterialRules.MaxTitleLinkLength);

        RuleFor(x => x.Link)
            .NotEmpty()
            .IsUrl(translator)
            .TrimWhitespace(translator);
    }
}
