using API.Interfaces;
using API.Models;
using Moq;

namespace APIUnitTests
{
    public class APIServiceTest
    {
        private readonly Mock<IAPIService> _apiServiceMock;

        public APIServiceTest()
        {
            _apiServiceMock = new Mock<IAPIService>();
        }

        [Fact]
        public async Task GetAllNotes_ShouldReturnListOfNotes()
        {
            // Arrange
            var expectedNotes = new List<NoteModel>
            {
                new NoteModel { NoteID = 1, NoteValue = "Test Note 1" },
                new NoteModel { NoteID = 2, NoteValue = "Test Note 2" }
            };
            _apiServiceMock.Setup(service => service.GetAllNotes()).ReturnsAsync(expectedNotes);

            // Act
            var actualNotes = await _apiServiceMock.Object.GetAllNotes();

            // Assert
            Assert.Equal(expectedNotes.Count, actualNotes.Count);
            for (int i = 0; i < expectedNotes.Count; i++)
            {
                Assert.Equal(expectedNotes[i].NoteID, actualNotes[i].NoteID);
                Assert.Equal(expectedNotes[i].NoteValue, actualNotes[i].NoteValue);
            }
        }

        [Fact]
        public async Task AddNote_ShouldAddNoteSuccessfully()
        {
            // Arrange
            var newNote = new NoteModel { NoteID = 3, NoteValue = "New Test Note" };
            _apiServiceMock.Setup(service => service.AddNoteAsync(newNote)).Returns(Task.CompletedTask);

            // Act
            await _apiServiceMock.Object.AddNoteAsync(newNote);

            // Assert
            _apiServiceMock.Verify(service => service.AddNoteAsync(newNote), Times.Once);
        }

        [Fact]
        public async Task UpdateNote_ShouldUpdateNoteSuccessfully()
        {
            // Arrange
            var updateNote = new UpdateNoteModel { NoteID = 1, NewValue = "Updated Test Note" };
            _apiServiceMock.Setup(service => service.UpdateNoteAsync(updateNote)).Returns(Task.CompletedTask);

            // Act
            await _apiServiceMock.Object.UpdateNoteAsync(updateNote);

            // Assert
            _apiServiceMock.Verify(service => service.UpdateNoteAsync(updateNote), Times.Once);
        }

        [Fact]
        public async Task DeleteNote_ShouldDeleteNoteSuccessfully()
        {
            // Arrange
            int noteIdToDelete = 1;
            _apiServiceMock.Setup(service => service.DeleteNoteAsync(noteIdToDelete)).Returns(Task.CompletedTask);

            // Act
            await _apiServiceMock.Object.DeleteNoteAsync(noteIdToDelete);

            // Assert
            _apiServiceMock.Verify(service => service.DeleteNoteAsync(noteIdToDelete), Times.Once);
        }
    }
}
