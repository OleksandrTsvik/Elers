using Application.Common.Extensions;
using Application.Common.Interfaces;
using Domain.Rules;
using FluentValidation;

namespace Application.CourseMaterials.CreateCourseMaterialFile;

public class CreateCourseMaterialFileCommandValidator : AbstractValidator<CreateCourseMaterialFileCommand>
{
    public CreateCourseMaterialFileCommandValidator(ITranslator translator)
    {
        RuleFor(x => x.TabId).NotEmpty();

        RuleFor(x => x.Title)
            .NotEmpty()
            .MinimumLength(CourseMaterialRules.MinTitleFileLength)
            .MaximumLength(CourseMaterialRules.MaxTitleFileLength)
            .TrimWhitespace(translator);

        RuleFor(x => x.File).NotEmpty();
    }
}
