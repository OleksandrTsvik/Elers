namespace API.Contracts;

public record SubmitAssignmentRequest(string? Text, IFormFile[]? Files);
