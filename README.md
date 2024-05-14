# CP-DOTNET-ASSESSMENT
ASP.NET core app using .NET 8 WEB API

In this project, Database connection and service initiation are done in the program.cs class. The API will work properly only when the project is initiated using the program.cs class.

The AppSetting.jon file has been utilized to set up the cosmos credentials as environment variables.

The Code can create a new database called "ApplicationManagementDB" if it doesn't exist and creates required cosmos containers for the project models.
Container names are **EmployerApplication** and **CandidateApplication**
All 4 API endpoints were tested using the Swagger tool while running the project.
Unit testing was not successful at the moment and the test cases are not included in this repository.

Thank you for the time to read this! Have a great day!
