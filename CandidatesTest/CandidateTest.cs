using CandidateManagementAPI.Controllers;
using CandidateManagementAPI.IServices;
using CandidateManagementAPI.Model;
using CandidateManagementAPI.NewFolder;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CandidatesTest
{
    public class CandidateTest
    {
        private readonly Mock<ICandidateRepository> _mockRepository;
        private readonly CandidatesController _controller;

        public CandidateTest()  // Corrected constructor name
        {
            _mockRepository = new Mock<ICandidateRepository>();
            _controller = new CandidatesController(_mockRepository.Object);
        }

        // Test for AddCandidateA (POST)
        [Fact]
        public async Task AddCandidateA_ReturnsBadRequest_WhenModelIsInvalid()
        {
            // Arrange
            var candidateDto = new CandidateDto(); // Simulate invalid data

            _controller.ModelState.AddModelError("FirstName", "First name is required.");

            // Act
            var result = await _controller.AddCandidateA(candidateDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);
        }

        [Fact]
        public async Task AddCandidateA_ReturnsOk_WhenCandidateIsCreated()
        {
            // Arrange
            var candidateDto = new CandidateDto { FirstName = "John", LastName = "Doe" }; // Valid candidate data
            _mockRepository.Setup(repo => repo.AddCandidateAsync(It.IsAny<CandidateDto>())).ReturnsAsync(1); // Mock repository

            // Act
            var result = await _controller.AddCandidateA(candidateDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Contains("Created Record Id", okResult.Value.ToString());
        }

        [Fact]
        public async Task AddCandidateA_ReturnsBadRequest_WhenFailedToCreateCandidate()
        {
            // Arrange
            var candidateDto = new CandidateDto { FirstName = "John", LastName = "Doe" };
            _mockRepository.Setup(repo => repo.AddCandidateAsync(It.IsAny<CandidateDto>())).ReturnsAsync(0); // Simulate failure

            // Act
            var result = await _controller.AddCandidateA(candidateDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);
        }

        // Test for UpdateCandidate (PUT)
        [Fact]
        public async Task UpdateCandidate_ReturnsBadRequest_WhenModelIsInvalid()
        {
            // Arrange
            var candidateDto = new CandidateDto();
            _controller.ModelState.AddModelError("FirstName", "First name is required.");

            // Act
            var result = await _controller.UpdateCandidate(1, candidateDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);
        }

        [Fact]
        public async Task UpdateCandidate_ReturnsNotFound_WhenCandidateDoesNotExist()
        {
            // Arrange
            var candidateDto = new CandidateDto { FirstName = "John", LastName = "Doe" };
            _mockRepository.Setup(repo => repo.UpdateCandidateAsync(It.IsAny<int>(), It.IsAny<CandidateDto>())).ReturnsAsync(0); // Simulate not found

            // Act
            var result = await _controller.UpdateCandidate(1, candidateDto);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task UpdateCandidate_ReturnsOk_WhenCandidateIsUpdated()
        {
            // Arrange
            var candidateDto = new CandidateDto { FirstName = "John", LastName = "Doe" };
            _mockRepository.Setup(repo => repo.UpdateCandidateAsync(It.IsAny<int>(), It.IsAny<CandidateDto>())).ReturnsAsync(1); // Mock successful update

            // Act
            var result = await _controller.UpdateCandidate(1, candidateDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Contains("sucessfully Updated", okResult.Value.ToString());
        }
    }
}
