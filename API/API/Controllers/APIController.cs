using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Interfaces;
using API.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIController : ControllerBase
    {
        private readonly IAPIService _apiService;

        public APIController(IAPIService apiService)
        {
            _apiService = apiService;
        }

        [HttpGet]
        public async Task<ActionResult<List<NoteModel>>> GetAllNotes()
        {
            var notes = await _apiService.GetAllNotes();
            return Ok(notes);
        }

        [HttpPost]
        public async Task<ActionResult> AddNote([FromBody] NoteModel note)
        {
            await _apiService.AddNoteAsync(note);
            return CreatedAtAction(nameof(GetAllNotes), new { id = note.NoteID }, note);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateNote([FromBody] UpdateNoteModel update)
        {
            await _apiService.UpdateNoteAsync(update);
            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteNote(int noteId)
        {
            await _apiService.DeleteNoteAsync(noteId);
            return NoContent();
        }
    }
}
