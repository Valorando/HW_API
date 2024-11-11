namespace API.Models
{
    public class NoteModel
    {
        public int NoteID { get; set; }
        public string NoteValue { get; set; }
    }

    public class UpdateNoteModel
    {
        public int NoteID { get; set; }
        public string NewValue { get; set; }
    }
}
