using FluentValidation;

namespace Application.CourseMaterials.CreateCourseMaterialContent;

public class CreateCourseMaterialContentCommandValidator
    : AbstractValidator<CreateCourseMaterialContentCommand>
{
    public CreateCourseMaterialContentCommandValidator()
    {
        RuleFor(x => x.TabId).NotEmpty();

        RuleFor(x => x.Content).NotEmpty();
    }
}
