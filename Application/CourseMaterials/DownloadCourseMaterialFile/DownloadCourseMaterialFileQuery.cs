using Application.Common.Messaging;
using Application.Common.Models;

namespace Application.CourseMaterials.DownloadCourseMaterialFile;

public record DownloadCourseMaterialFileQuery(string FileName) : IQuery<FileDownloadResult>;
