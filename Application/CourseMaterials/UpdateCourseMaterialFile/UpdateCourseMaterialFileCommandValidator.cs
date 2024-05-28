using Application.Common.Extensions;
using Application.Common.Interfaces;
using Domain.Rules;
using FluentValidation;

namespace Application.CourseMaterials.UpdateCourseMaterialFile;

public class UpdateCourseMaterialFileCommandValidator : AbstractValidator<UpdateCourseMaterialFileCommand>
{
    public UpdateCourseMaterialFileCommandValidator(ITranslator translator)
    {
        RuleFor(x => x.Id).NotEmpty();

        RuleFor(x => x.Title)
            .NotEmpty()
            .MinimumLength(CourseMaterialRules.MinTitleFileLength)
            .MaximumLength(CourseMaterialRules.MaxTitleFileLength)
            .TrimWhitespace(translator);
    }
}
