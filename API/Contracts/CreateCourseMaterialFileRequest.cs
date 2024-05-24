namespace API.Contracts;

public record CreateCourseMaterialFileRequest(string Title, IFormFile File);
