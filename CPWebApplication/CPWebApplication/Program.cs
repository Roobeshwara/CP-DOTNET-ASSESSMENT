using CPWebApplication.Interfaces;
using CPWebApplication.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//DI
builder.Services.AddSingleton<ICandidateApplicationService, CandidateApplicationService>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//Initializing the required services
AppSettings.Initialize(app.Configuration);
await CosmosDBConnectionService.GetStartedCosmosDBAsync();
CandidateApplicationService.Initialize(CosmosDBConnectionService.CandidateApplicationContainer);


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
