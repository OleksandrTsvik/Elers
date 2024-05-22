using Application.Common.Interfaces;
using Application.Common.Messaging;
using Domain.Repositories;
using Domain.Shared;

namespace Application.CourseMaterials.DeleteCourseMaterial;

public class DeleteCourseMaterialCommandHandler : ICommandHandler<DeleteCourseMaterialCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICourseMaterialRepository _courseMaterialRepository;

    public DeleteCourseMaterialCommandHandler(
        IUnitOfWork unitOfWork,
        ICourseMaterialRepository courseMaterialRepository)
    {
        _unitOfWork = unitOfWork;
        _courseMaterialRepository = courseMaterialRepository;
    }

    public async Task<Result> Handle(DeleteCourseMaterialCommand request, CancellationToken cancellationToken)
    {
        await _courseMaterialRepository.RemoveAsync(request.Id, cancellationToken);

        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}
