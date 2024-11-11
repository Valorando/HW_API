using API.Interfaces;
using API.Models;

namespace API.Services
{
    public class APIService : IAPIService
    {
        private readonly string _connectionString;
        private readonly Dictionary<string, string> _sqlQueries;

        public APIService(string connectionString, Dictionary<string, string> sqlQueries)
        {
            _connectionString = connectionString;
            _sqlQueries = sqlQueries;
        }

        public void AddNote(NoteModel note)
        {
            throw new NotImplementedException();
        }

        public void DeleteNote(int noteId)
        {
            throw new NotImplementedException();
        }

        public Task<List<NoteModel>> GetAllNotes()
        {
            throw new NotImplementedException();
        }

        public void UpdateNote(UpdateNoteModel update)
        {
            throw new NotImplementedException();
        }
    }
}
