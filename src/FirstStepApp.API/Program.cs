using FirstStepApp.API.Infrastructure;
using FirstStepApp.API.Models;
using FirstStepApp.API.Services;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddHttpClient<OllamaClient>();
builder.Services.AddScoped<IOllama, OllamaClient>();
// Register your service (assuming _ollamaService is part of a service you need to inject)
builder.Services.AddScoped<IOllamaService, OllamaService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapPost("/generate-response", async (IOllamaService ollamaService, [FromBody] Idea input) =>
{
    var response = await ollamaService.GenerateResponseAsync(input.Description ?? string.Empty);
    if (string.IsNullOrEmpty(response))
    {
        return Results.BadRequest("Failed to generate a response.");
    }
    return Results.Ok(response);
})
.WithName("GenerateResponse");

app.Run();


