using API.Controllers;
using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace APIUnitTests
{
    public class ControllerTest
    {
        private readonly APIController _apiController;
        private readonly Mock<IAPIService> _apiServiceMock;

        public ControllerTest()
        {
            _apiServiceMock = new Mock<IAPIService>();
            _apiController = new APIController(_apiServiceMock.Object);
        }

        [Fact]
        public async Task GetAllNotesTest()
        {
            // Arrange
            var expectedNotes = new List<NoteModel>
            {
                new NoteModel { NoteID = 1, NoteValue = "Test Note 1" },
                new NoteModel { NoteID = 2, NoteValue = "Test Note 2" }
            };
            _apiServiceMock.Setup(service => service.GetAllNotes()).ReturnsAsync(expectedNotes);

            // Act
            var actionResult = await _apiController.GetAllNotes();

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var actualNotes = Assert.IsAssignableFrom<List<NoteModel>>(okObjectResult.Value);
            Assert.Equal(expectedNotes.Count, actualNotes.Count);
        }

        [Fact]
        public async Task AddNoteTest()
        {
            // Arrange
            var newNote = new NoteModel { NoteID = 3, NoteValue = "New Test Note" };
            _apiServiceMock.Setup(service => service.AddNoteAsync(newNote)).Returns(Task.CompletedTask);

            // Act
            var actionResult = await _apiController.AddNote(newNote);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(actionResult);
            Assert.Equal(nameof(_apiController.GetAllNotes), createdAtActionResult.ActionName);
            var returnedNote = Assert.IsType<NoteModel>(createdAtActionResult.Value);
            Assert.Equal(newNote.NoteID, returnedNote.NoteID);
            Assert.Equal(newNote.NoteValue, returnedNote.NoteValue);
        }

        [Fact]
        public async Task UpdateNoteTest()
        {
            // Arrange
            var updateNote = new UpdateNoteModel { NoteID = 1, NewValue = "Updated Test Note" };
            _apiServiceMock.Setup(service => service.UpdateNoteAsync(updateNote)).Returns(Task.CompletedTask);

            // Act
            var actionResult = await _apiController.UpdateNote(updateNote);

            // Assert
            Assert.IsType<NoContentResult>(actionResult);
            _apiServiceMock.Verify(service => service.UpdateNoteAsync(updateNote), Times.Once);
        }

        [Fact]
        public async Task DeleteNoteTest()
        {
            // Arrange
            int noteIdToDelete = 1;
            _apiServiceMock.Setup(service => service.DeleteNoteAsync(noteIdToDelete)).Returns(Task.CompletedTask);

            // Act
            var actionResult = await _apiController.DeleteNote(noteIdToDelete);

            // Assert
            Assert.IsType<NoContentResult>(actionResult);
            _apiServiceMock.Verify(service => service.DeleteNoteAsync(noteIdToDelete), Times.Once);
        }
    }
}
