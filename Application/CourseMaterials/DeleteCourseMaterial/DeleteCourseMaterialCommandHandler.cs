using Application.Common.Messaging;
using Application.Common.Services;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.CourseMaterials.DeleteCourseMaterial;

public class DeleteCourseMaterialCommandHandler : ICommandHandler<DeleteCourseMaterialCommand>
{
    private readonly ICourseMaterialRepository _courseMaterialRepository;
    private readonly ISubmittedAssignmentRepository _submittedAssignmentRepository;
    private readonly IGradeRepository _gradeRepository;
    private readonly ITestQuestionRepository _testQuestionRepository;
    private readonly ITestSessionRespository _testSessionRespository;
    private readonly IFileService _fileService;

    public DeleteCourseMaterialCommandHandler(
        ICourseMaterialRepository courseMaterialRepository,
        ISubmittedAssignmentRepository submittedAssignmentRepository,
        IGradeRepository gradeRepository,
        ITestQuestionRepository testQuestionRepository,
        ITestSessionRespository testSessionRespository,
        IFileService fileService)
    {
        _courseMaterialRepository = courseMaterialRepository;
        _submittedAssignmentRepository = submittedAssignmentRepository;
        _gradeRepository = gradeRepository;
        _testQuestionRepository = testQuestionRepository;
        _testSessionRespository = testSessionRespository;
        _fileService = fileService;
    }

    public async Task<Result> Handle(DeleteCourseMaterialCommand request, CancellationToken cancellationToken)
    {
        CourseMaterial? courseMaterial = await _courseMaterialRepository.GetByIdAsync(
            request.Id, cancellationToken);

        if (courseMaterial is null)
        {
            return CourseMaterialErrors.NotFound(request.Id);
        }

        if (courseMaterial is CourseMaterialFile courseMaterialFile)
        {
            await _fileService.RemoveAsync(courseMaterialFile.UniqueFileName, cancellationToken);
        }
        else if (courseMaterial is CourseMaterialAssignment courseMaterialAssignment)
        {
            List<string> uniqueFileNames = await _submittedAssignmentRepository
                .GetSubmittedFilesByAssignmentIdAsync(courseMaterialAssignment.Id, cancellationToken);

            if (uniqueFileNames.Count != 0)
            {
                await _fileService.RemoveRangeAsync(uniqueFileNames, cancellationToken);
            }

            await _gradeRepository.RemoveRangeByAssignmentIdAsync(
                courseMaterialAssignment.Id, cancellationToken);

            await _submittedAssignmentRepository.RemoveRangeByAssignmentIdAsync(
                courseMaterialAssignment.Id, cancellationToken);
        }
        else if (courseMaterial is CourseMaterialTest courseMaterialTest)
        {
            await _gradeRepository.RemoveRangeByTestIdAsync(courseMaterialTest.Id, cancellationToken);
            await _testSessionRespository.RemoveRangeByTestIdAsync(courseMaterialTest.Id, cancellationToken);
            await _testQuestionRepository.RemoveRangeByTestIdAsync(courseMaterialTest.Id, cancellationToken);
        }

        await _courseMaterialRepository.RemoveAsync(courseMaterial.Id, cancellationToken);

        return Result.Success();
    }
}
