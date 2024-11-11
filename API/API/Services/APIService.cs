using API.Interfaces;
using API.Models;
using MySql.Data.MySqlClient;
using System.Data;

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

        public async Task AddNoteAsync(NoteModel note)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new MySqlCommand(_sqlQueries["AddNote"], connection))
                {
                    command.Parameters.AddWithValue("@noteid", note.NoteID);
                    command.Parameters.AddWithValue("@note", note.NoteValue);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteNoteAsync(int noteId)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new MySqlCommand(_sqlQueries["DeleteNote"], connection))
                {
                    command.Parameters.AddWithValue("@noteid", noteId);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<List<NoteModel>> GetAllNotes()
        {
            var notes = new List<NoteModel>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new MySqlCommand(_sqlQueries["GetAllNotes"], connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            notes.Add(new NoteModel
                            {
                                NoteID = reader.GetInt32("noteid"),
                                NoteValue = reader.GetString("note")
                            });
                        }
                    }
                }
            }

            return notes;
        }

        public async Task UpdateNoteAsync(UpdateNoteModel update)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new MySqlCommand(_sqlQueries["UpdateNote"], connection))
                {
                    command.Parameters.AddWithValue("@noteid", update.NoteID);
                    command.Parameters.AddWithValue("@note", update.NewValue);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
