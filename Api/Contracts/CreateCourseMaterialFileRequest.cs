namespace Api.Contracts;

public record CreateCourseMaterialFileRequest(string Title, IFormFile File);
