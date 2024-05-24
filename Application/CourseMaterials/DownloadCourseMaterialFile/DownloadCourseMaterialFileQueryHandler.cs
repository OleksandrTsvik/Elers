using Application.Common.Messaging;
using Application.Common.Models;
using Application.Common.Queries;
using Application.Common.Services;
using Domain.Errors;
using Domain.Shared;

namespace Application.CourseMaterials.DownloadCourseMaterialFile;

public class DownloadCourseMaterialFileQueryHandler
    : IQueryHandler<DownloadCourseMaterialFileQuery, FileDownloadResult>
{
    private readonly ICourseMaterialQueries _courseMaterialQueries;
    private readonly IFileService _fileService;

    public DownloadCourseMaterialFileQueryHandler(
        ICourseMaterialQueries courseMaterialQueries,
        IFileService fileService)
    {
        _courseMaterialQueries = courseMaterialQueries;
        _fileService = fileService;
    }

    public async Task<Result<FileDownloadResult>> Handle(
        DownloadCourseMaterialFileQuery request,
        CancellationToken cancellationToken)
    {
        GetCourseMaterialFileInfoDto? courseMaterialFile = await _courseMaterialQueries
            .GetCourseMaterialFileInfo(request.FileName, cancellationToken);

        if (courseMaterialFile is null)
        {
            return CourseMaterialErrors.FileNotFound();
        }

        byte[] fileContents = await _fileService.DownloadAsync(
            courseMaterialFile.UniqueFileName, cancellationToken);

        return new FileDownloadResult
        {
            FileName = courseMaterialFile.FileName,
            FileContents = fileContents
        };
    }
}
