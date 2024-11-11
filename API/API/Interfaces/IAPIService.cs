using API.Models;
namespace API.Interfaces
{
    public interface IAPIService
    {
        public Task<List<NoteModel>> GetAllNotes();
        public Task AddNoteAsync(NoteModel note);
        public Task UpdateNoteAsync(UpdateNoteModel update);
        public Task DeleteNoteAsync(int noteId);
    }
}
