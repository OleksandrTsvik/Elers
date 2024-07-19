namespace Api.Contracts;

public record UpdateCourseMaterialFileRequest(string Title, IFormFile? File);
