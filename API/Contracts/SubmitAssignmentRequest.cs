namespace Api.Contracts;

public record SubmitAssignmentRequest(string? Text, IFormFile[]? Files);
