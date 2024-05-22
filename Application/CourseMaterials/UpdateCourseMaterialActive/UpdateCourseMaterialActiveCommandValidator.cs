using FluentValidation;

namespace Application.CourseMaterials.UpdateCourseMaterialActive;

public class UpdateCourseMaterialActiveCommandValidator
    : AbstractValidator<UpdateCourseMaterialActiveCommand>
{
    public UpdateCourseMaterialActiveCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();

        RuleFor(x => x.IsActive).NotNull();
    }
}
