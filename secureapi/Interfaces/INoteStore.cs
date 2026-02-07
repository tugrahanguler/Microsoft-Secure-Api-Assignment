using MyApi.Api.Models;

namespace MyApi.Api.Interfaces;

public interface INoteStore
{
    void Add(Note note);
    IEnumerable<Note> GetMine(string ownerUsername);
}
