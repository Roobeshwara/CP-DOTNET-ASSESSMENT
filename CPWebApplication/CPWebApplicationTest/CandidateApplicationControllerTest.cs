using CPWebApplication.Controllers;
using CPWebApplication.Interfaces;
using CPWebApplication.Models;
using CPWebApplication.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos.Serialization.HybridRow;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPWebApplicationTest
{
    public class CandidateApplicationControllerTest
    {
        CandidateApplicationController _controller;
        ICandidateApplicationService _candidateApplicationService;
        public CandidateApplicationControllerTest()
        {
            // Create a mock IConfiguration using the factory method with appsettings.json values
            var settings = new Dictionary<string, string>
            {
                ["Logging:LogLevel:Default"] = "Information",
                ["Logging:LogLevel:Microsoft.AspNetCore"] = "Warning",
                ["AllowedHosts"] = "*",
                ["CosmosCredentials:EndpointUri"] = "https://localhost:8081",
                ["CosmosCredentials:PrimaryKey"] = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw=="
            };
            var configuration = MockConfigurationFactory.CreateMockConfiguration(settings);

            _candidateApplicationService = new CandidateApplicationService(configuration);
            _controller = new CandidateApplicationController(_candidateApplicationService);
        }
        [Fact]
        public async Task AddCandidateApplication_Success()
        {
            // Arrange
            var application = new CandidateApplication
            {
                id = "14",
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "+1234567890",
                Nationality = "American",
                CurrentResidance = "New York",
                IDNumber = "ABC123XYZ",
                DateOFBirth = DateTime.Parse("1990-05-25"),
                Gender = "Male",
                AbountCandidate = "I am a dedicated professional seeking new opportunities.",
                YearOfGraduation = "2015",
                CandidateInterest = new List<string> { "Software Development", "Data Science", "Machine Learning" },
                IsRejectedByUkEmbassy = false,
                YearsOfExperince = 6,
                DateMovedToUK = DateTime.Parse("2021-03-15")
            };
            // Act
            var result = await _controller.AddCandidateApplication(application);

            // Assert
            // Check that the IActionResult is of type OkObjectResult
            Assert.IsType<OkObjectResult>(result);

            // Cast the result to OkObjectResult and verify its value
            var okResult = result as OkObjectResult;
            Assert.Equal("Record Inserted", okResult.Value);
        }

        [Fact]
        public async Task AddCandidateApplication_Exception()
        {
            // Arrange
            //Using already taken Id
            var application = new CandidateApplication
            {
                id = "2",
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "+1234567890",
                Nationality = "American",
                CurrentResidance = "New York",
                IDNumber = "ABC123XYZ",
                DateOFBirth = DateTime.Parse("1990-05-25"),
                Gender = "Male",
                AbountCandidate = "I am a dedicated professional seeking new opportunities.",
                YearOfGraduation = "2015",
                CandidateInterest = new List<string> { "Software Development", "Data Science", "Machine Learning" },
                IsRejectedByUkEmbassy = false,
                YearsOfExperince = 6,
                DateMovedToUK = DateTime.Parse("2021-03-15")
            };

            // Act

            var result = await _controller.AddCandidateApplication(application);

            // Assert

            // Check that the IActionResult is of type BadRequestObjectResult
            Assert.IsType<BadRequestObjectResult>(result);
            // Cast the result to BadRequestObjectResult and verify its value
            var badRequestResult = result as BadRequestObjectResult;
        }
    }
}
