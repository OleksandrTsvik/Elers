using FluentValidation;

namespace Application.CourseMaterials.UpdateCourseMaterialContent;

public class UpdateCourseMaterialContentCommandValidator
    : AbstractValidator<UpdateCourseMaterialContentCommand>
{
    public UpdateCourseMaterialContentCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();

        RuleFor(x => x.Content).NotEmpty();
    }
}
