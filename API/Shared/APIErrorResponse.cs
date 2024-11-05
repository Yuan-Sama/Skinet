namespace API.Shared;

public record APIErrorResponse(int StatusCode, string Message, string? Details);
