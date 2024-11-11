using API.Models;
namespace API.Interfaces
{
    public interface IAPIService
    {
        public Task<List<NoteModel>> GetAllNotes();
        public void AddNote(NoteModel note);
        public void UpdateNote(UpdateNoteModel update);
        public void DeleteNote(int noteId);
    }
}
