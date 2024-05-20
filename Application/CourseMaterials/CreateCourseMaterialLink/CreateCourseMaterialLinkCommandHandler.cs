using Application.Common.Interfaces;
using Application.Common.Messaging;
using Domain.Entities;
using Domain.Repositories;
using Domain.Shared;

namespace Application.CourseMaterials.CreateCourseMaterialLink;

public class CreateCourseMaterialLinkCommandHandler
    : ICommandHandler<CreateCourseMaterialLinkCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICourseMaterialRepository _courseMaterialRepository;

    public CreateCourseMaterialLinkCommandHandler(
        IUnitOfWork unitOfWork,
        ICourseMaterialRepository courseMaterialRepository)
    {
        _unitOfWork = unitOfWork;
        _courseMaterialRepository = courseMaterialRepository;
    }

    public async Task<Result> Handle(
        CreateCourseMaterialLinkCommand request,
        CancellationToken cancellationToken)
    {
        var course = new CourseMaterialLink
        {
            CourseTabId = request.TabId,
            Title = request.Title,
            Link = request.Link
        };

        await _courseMaterialRepository.AddAsync(course, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
