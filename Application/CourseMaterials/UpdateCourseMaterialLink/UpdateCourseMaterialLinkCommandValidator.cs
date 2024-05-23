using Application.Common.Extensions;
using Domain.Rules;
using FluentValidation;

namespace Application.CourseMaterials.UpdateCourseMaterialLink;

public class UpdateCourseMaterialLinkCommandValidator : AbstractValidator<UpdateCourseMaterialLinkCommand>
{
    public UpdateCourseMaterialLinkCommandValidator(Common.Interfaces.ITranslator translator)
    {
        RuleFor(x => x.Id).NotEmpty();

        RuleFor(x => x.Title)
            .NotEmpty()
            .MinimumLength(CourseMaterialRules.MinTitleLinkLength)
            .MaximumLength(CourseMaterialRules.MaxTitleLinkLength);

        RuleFor(x => x.Link)
            .NotEmpty()
            .MaximumLength(CourseMaterialRules.MaxLinkLength)
            .IsUrl(translator)
            .TrimWhitespace(translator);
    }
}
