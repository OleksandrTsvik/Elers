using Application.Common.Interfaces;
using Application.Common.Messaging;
using Domain.Entities;
using Domain.Repositories;
using Domain.Shared;

namespace Application.CourseMaterials.CreateCourseMaterialContent;

public class CreateCourseMaterialContentCommandHandler
    : ICommandHandler<CreateCourseMaterialContentCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICourseMaterialRepository _courseMaterialRepository;

    public CreateCourseMaterialContentCommandHandler(
        IUnitOfWork unitOfWork,
        ICourseMaterialRepository courseMaterialRepository)
    {
        _unitOfWork = unitOfWork;
        _courseMaterialRepository = courseMaterialRepository;
    }

    public async Task<Result> Handle(
        CreateCourseMaterialContentCommand request,
        CancellationToken cancellationToken)
    {
        var course = new CourseMaterialContent
        {
            CourseTabId = request.TabId,
            Content = request.Content
        };

        await _courseMaterialRepository.AddAsync(course, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
