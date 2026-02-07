namespace MyApi.Api.Dtos;

public record CreateNoteRequest(string Content);
public record NoteResponse(Guid Id, string Content);
