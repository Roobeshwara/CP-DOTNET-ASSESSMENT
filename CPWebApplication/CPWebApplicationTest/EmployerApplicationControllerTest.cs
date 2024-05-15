using CPWebApplication.Controllers;
using CPWebApplication.Interfaces;
using CPWebApplication.Models;
using CPWebApplication.Services;
using Microsoft.AspNetCore.Mvc;

namespace CPWebApplicationTest
{
    public class EmployerApplicationControllerTest
    {
        EmployerApplicationController _controller;
        IEmployerApplicationService _employerApplicationService;
        public EmployerApplicationControllerTest()
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

            _employerApplicationService = new EmployerApplicationSerivce(configuration);
            _controller = new EmployerApplicationController(_employerApplicationService);
        }
        [Fact]
        public async void AddEmployerApplicationAsync_Success()
        {
            //Arrange
            var application = new EmployerApplication
            {
                id = "1000",
                ProgramTitle = "Software Engineering Internship",
                ProgramDescription = "Join our team to gain valuable experience in software development.",
                FirstName = "Roobeshwara",
                LastName = "Sharma",
                Email = "john.doe@example.com",
                Phone = "+1234567890",
                Nationality = "American",
                CurrentResidance = "New York",
                IDNumber = "ABC123XYZ",
                DateOFBirth = DateTime.Parse("1990-05-25"),
                Gender = "Male",
                Questions = new List<QuestionModel>
                {
                    new QuestionModel
                    {
                        id = "1",
                        Type = "Dropdown",
                        Question = "Select your preferred programming language:",
                        Choices = new List<string> {"Java", "Python", "C#", "JavaScript"},
                        EnableOtherOption = false
                    },
                    new QuestionModel
                    {
                        id = "2",
                        Type = "MultipleChoice",
                        Question = "Which areas of software development are you interested in?",
                        Choices = new List<string> {"Web Development", "Mobile App Development", "Data Science", "Game Development"},
                        EnableOtherOption = true,
                        MaxChoiceAllowed = 2
                    },
                    new QuestionModel
                    {
                        id = "3",
                        Type = "Paragraph",
                        Question = "Please describe your previous experience in software development:"
                    },
                    new QuestionModel
                    {
                        id = "4",
                        Type = "Date",
                        Question = "When are you available to start?"
                    },
                    new QuestionModel
                    {
                        id = "5",
                        Type = "Dropdown",
                        Question = "What is your highest level of education?",
                        Choices = new List<string> {"High School", "Bachelor's Degree", "Master's Degree", "Ph.D."},
                        EnableOtherOption = true
                    },
                    new QuestionModel
                    {
                        id = "6",
                        Type = "MultipleChoice",
                        Question = "Which programming languages are you proficient in?",
                        Choices = new List<string> {"Java", "Python", "C#", "JavaScript", "Ruby", "PHP", "Swift", "Kotlin", "Go", "Rust"},
                        EnableOtherOption = true,
                        MaxChoiceAllowed = 3
                    }
                }
            };
            //Act

            var actionResult = await _controller.AddEmployerApplication(application);

            // Assert

            // Check that the IActionResult is of type OkObjectResult
            Assert.IsType<OkObjectResult>(actionResult);
            // Cast the result to OkObjectResult and verify its value
            var okResult = actionResult as OkObjectResult;
            Assert.Equal("Record Inserted", okResult.Value);
        }
        [Fact]
        public async void AddEmployerApplicationAsync_Exception()
        {
            //Arrange
            var application = new EmployerApplication
            {
                id = "1",
                ProgramTitle = "Software Engineering Internship",
                ProgramDescription = "Join our team to gain valuable experience in software development.",
                FirstName = "Roobeshwara",
                LastName = "Sharma",
                Email = "john.doe@example.com",
                Phone = "+1234567890",
                Nationality = "American",
                CurrentResidance = "New York",
                IDNumber = "ABC123XYZ",
                DateOFBirth = DateTime.Parse("1990-05-25"),
                Gender = "Male",
                Questions = new List<QuestionModel>
                {
                    new QuestionModel
                    {
                        id = "1",
                        Type = "Dropdown",
                        Question = "Select your preferred programming language:",
                        Choices = new List<string> {"Java", "Python", "C#", "JavaScript"},
                        EnableOtherOption = false
                    },
                    new QuestionModel
                    {
                        id = "2",
                        Type = "MultipleChoice",
                        Question = "Which areas of software development are you interested in?",
                        Choices = new List<string> {"Web Development", "Mobile App Development", "Data Science", "Game Development"},
                        EnableOtherOption = true,
                        MaxChoiceAllowed = 2
                    },
                    new QuestionModel
                    {
                        id = "3",
                        Type = "Paragraph",
                        Question = "Please describe your previous experience in software development:"
                    },
                    new QuestionModel
                    {
                        id = "4",
                        Type = "Date",
                        Question = "When are you available to start?"
                    },
                    new QuestionModel
                    {
                        id = "5",
                        Type = "Dropdown",
                        Question = "What is your highest level of education?",
                        Choices = new List<string> {"High School", "Bachelor's Degree", "Master's Degree", "Ph.D."},
                        EnableOtherOption = true
                    },
                    new QuestionModel
                    {
                        id = "6",
                        Type = "MultipleChoice",
                        Question = "Which programming languages are you proficient in?",
                        Choices = new List<string> {"Java", "Python", "C#", "JavaScript", "Ruby", "PHP", "Swift", "Kotlin", "Go", "Rust"},
                        EnableOtherOption = true,
                        MaxChoiceAllowed = 3
                    }
                }
            };
            //Act

            var actionResult = await _controller.AddEmployerApplication(application);

            // Assert

            // Check that the IActionResult is of type OkObjectResult
            Assert.IsType<ConflictObjectResult>(actionResult);
            // Cast the result to OkObjectResult and verify its value
            var duplicateResult = actionResult as ConflictObjectResult;
            Assert.Equal("Duplicate record. Record already exists.", duplicateResult.Value);
        }
        [Fact]
        public async Task UpdateEmployerApplicationAsync_Success()
        {
            // Arrange
            var application = new EmployerApplication
            {
                id = "1",
                ProgramTitle = "Software Engineering Internship",
                ProgramDescription = "Join our team to gain valuable experience in software development.",
                FirstName = "Roobeshwara",
                LastName = "Sharma",
                Email = "john.doe@example.com",
                Phone = "+1234567890",
                Nationality = "American",
                CurrentResidance = "New York",
                IDNumber = "ABC123XYZ",
                DateOFBirth = DateTime.Parse("1990-05-25"),
                Gender = "Male",
                Questions = new List<QuestionModel>
                {
                    new QuestionModel
                    {
                        id = "1",
                        Type = "Dropdown",
                        Question = "Select your preferred programming language:",
                        Choices = new List<string> {"Java", "Python", "C#", "JavaScript"},
                        EnableOtherOption = false
                    },
                    new QuestionModel
                    {
                        id = "2",
                        Type = "MultipleChoice",
                        Question = "Which areas of software development are you interested in?",
                        Choices = new List<string> {"Web Development", "Mobile App Development", "Data Science", "Game Development"},
                        EnableOtherOption = true,
                        MaxChoiceAllowed = 2
                    },
                    new QuestionModel
                    {
                        id = "3",
                        Type = "Paragraph",
                        Question = "Please describe your previous experience in software development:"
                    },
                    new QuestionModel
                    {
                        id = "4",
                        Type = "Date",
                        Question = "When are you available to start?"
                    }
                }
            };

            // Act
            var result = await _controller.UpdateEmployerApplication(application);

            // Assert
            // Check that the IActionResult is of type OkObjectResult
            Assert.IsType<OkObjectResult>(result);

            // Cast the result to OkObjectResult and verify its value
            var okResult = result as OkObjectResult;
            Assert.IsAssignableFrom<EmployerApplication>(okResult.Value);
        }
        [Fact]
        public async Task UpdateEmployerApplicationAsync_Exception()
        {
            // Arrange
            var application = new EmployerApplication
            {
                //ID which is not available
                id = "5",
                ProgramTitle = "Software Engineering Internship Updated",
                ProgramDescription = "Join our team to gain valuable experience in software development.",
                FirstName = "Roobeshwara",
                LastName = "Sharma",
                Email = "john.doe@example.com",
                Phone = "+1234567890",
                Nationality = "American",
                CurrentResidance = "New York",
                IDNumber = "ABC123XYZ",
                DateOFBirth = DateTime.Parse("1990-05-25"),
                Gender = "Male",
                Questions = new List<QuestionModel>
                {
                    new QuestionModel
                    {
                        id = "1",
                        Type = "Dropdown",
                        Question = "Select your preferred programming language:",
                        Choices = new List<string> {"Java", "Python", "C#", "JavaScript"},
                        EnableOtherOption = false
                    },
                    new QuestionModel
                    {
                        id = "2",
                        Type = "MultipleChoice",
                        Question = "Which areas of software development are you interested in?",
                        Choices = new List<string> {"Web Development", "Mobile App Development", "Data Science", "Game Development"},
                        EnableOtherOption = true,
                        MaxChoiceAllowed = 2
                    },
                    new QuestionModel
                    {
                        id = "3",
                        Type = "Paragraph",
                        Question = "Please describe your previous experience in software development:"
                    },
                    new QuestionModel
                    {
                        id = "4",
                        Type = "Date",
                        Question = "When are you available to start?"
                    }
                }
            };

            // Act
            var result = await _controller.UpdateEmployerApplication(application);

            // Assert
            // Check that the IActionResult is of type BadRequestObjectResult
            Assert.IsType<NotFoundObjectResult>(result);

            // Cast the result to BadRequestObjectResult and verify its value
            var notFoundResult = result as NotFoundObjectResult;
            Assert.Equal("Employer application not found.", notFoundResult.Value);

        }
        [Fact]
        public async Task GetQuestionsByType_Success()
        {
            // Arrange
            var applicationId = "1";
            var questionType = "Dropdown";

            // Act
            var result = await _controller.GetQuestionsByType(applicationId, questionType);

            // Assert
            // Check that the IActionResult is of type OkObjectResult
            Assert.IsType<OkObjectResult>(result);

            // Cast the result to OkObjectResult and verify its value
            var okResult = result as OkObjectResult;
            Assert.IsAssignableFrom<List<QuestionModel>>(okResult.Value);
        }
        [Fact]
        public async Task GetQuestionsByType_Exception()
        {
            // Arrange
            var applicationId = "1000";
            var questionType = "Dropdown";

            // Act
            var result = await _controller.GetQuestionsByType(applicationId, questionType);

            // Assert
            // Check that the IActionResult is of type OkObjectResult
            Assert.IsType<NotFoundObjectResult>(result);

            // Cast the result to OkObjectResult and verify its value
            var notFoundResult = result as NotFoundObjectResult;
            Assert.Equal("Employer application not found.", notFoundResult.Value);
        }
    }
}