namespace API.Contracts;

public record UpdateCourseMaterialFileRequest(string Title, IFormFile? File);
