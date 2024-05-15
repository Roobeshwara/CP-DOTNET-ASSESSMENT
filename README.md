# CP-DOTNET-ASSESSMENT
ASP.NET core app using .NET 8 WEB API

In this project, Database connection and service initiation are done in the program.cs class. The API will work properly only when the project is initiated using the program.cs class.

The AppSetting.jon file has been utilized to set up the cosmos credentials as environment variables.

The Code can create a new database called "ApplicationManagementDB" if it doesn't exist and creates required cosmos containers for the project models.
Container names are **EmployerApplication** and **CandidateApplication**
All 4 API endpoints were tested using the Swagger tool while running the project.

Basic unit test cases are included in this project using xunit testing.

To run this app in any device, please update the AppSettings with the Cosmose credetials

**"CosmosCredentials": {
  "EndpointUri": "https://localhost:8081",
  "PrimaryKey": "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw=="
}**

Thank you for the time to read this! Have a great day!
