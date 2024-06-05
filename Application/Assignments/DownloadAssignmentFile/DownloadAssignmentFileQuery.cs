using Application.Common.Messaging;
using Application.Common.Models;

namespace Application.Assignments.DownloadAssignmentFile;

public record DownloadAssignmentFileQuery(string FileName) : IQuery<FileDownloadResult>;
