using System.Collections.Concurrent;
using MyApi.Api.Interfaces;
using MyApi.Api.Models;

namespace MyApi.Api.Services;

public class InMemoryNoteStore : INoteStore
{
    private readonly ConcurrentBag<Note> _notes = new();

    public void Add(Note note) => _notes.Add(note);

    public IEnumerable<Note> GetMine(string ownerUsername)
        => _notes.Where(n => string.Equals(n.OwnerUsername, ownerUsername, StringComparison.OrdinalIgnoreCase));
}
