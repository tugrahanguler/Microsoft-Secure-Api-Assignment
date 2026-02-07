namespace MyApi.Api.Models;

public class Note
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string OwnerUsername { get; set; } = default!;
    public string Content { get; set; } = default!;
}
