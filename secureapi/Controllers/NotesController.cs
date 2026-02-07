using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApi.Api.Dtos;
using MyApi.Api.Interfaces;
using MyApi.Api.Models;
using MyApi.Api.Services;

namespace MyApi.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class NotesController : ControllerBase
{
    private readonly INoteStore _store;

    public NotesController(INoteStore store) => _store = store;

    [HttpPost]
    public IActionResult Create([FromBody] CreateNoteRequest req)
    {
        if (string.IsNullOrWhiteSpace(req.Content))
            return BadRequest(new { message = "Content is required." });

        if (InputGuards.ContainsHtml(req.Content))
            return BadRequest(new { message = "HTML is not allowed in notes." });

        var owner = User.Identity!.Name!;
        var note = new Note { OwnerUsername = owner, Content = req.Content };
        _store.Add(note);

        return Ok(new NoteResponse(note.Id, note.Content));
    }

    [HttpGet("mine")]
    public IActionResult Mine()
    {
        var owner = User.Identity!.Name!;
        var notes = _store.GetMine(owner).Select(n => new NoteResponse(n.Id, n.Content));
        return Ok(notes);
    }
}
