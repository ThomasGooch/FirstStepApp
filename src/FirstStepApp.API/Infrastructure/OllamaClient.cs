using System;
using AutoGen.Core;
using AutoGen.Ollama;
using AutoGen.Ollama.Extension;

namespace FirstStepApp.API.Infrastructure;

public class OllamaClient : IOllama
{
    private readonly IStreamingAgent _developerAgent;
    private readonly IStreamingAgent _unitTestAgent;

    public OllamaClient(HttpClient httpClient)
    {
        if (httpClient.BaseAddress == null)
        {
            httpClient.BaseAddress = new Uri("http://localhost:11434");
        }

        _developerAgent = new OllamaAgent(
            httpClient: httpClient,
            name: "DeveloperAgent",
            modelName: "llama3.1",
            systemMessage: @"You are a professional dotnet engineer, known for your expertise in 
software development. You often work on projects that require you to build in .Net9 using Clean Architecture.
You use your skills to create software applications, tools, and 
games that are both functional and efficient.
Your preference is to write clean, well-structured code that is easy 
to read and maintain."
        )
        .RegisterMessageConnector()
        .RegisterPrintMessage();

        _unitTestAgent = new OllamaAgent(
            httpClient: httpClient,
            name: "UnitTestAgent",
            modelName: "llama3.1",
            systemMessage: @"You are a professional dotnet engineer, known for your expertise in unit testing dotnet applications.
You often work on projects that require you to build in .Net9 as a version.
You use your skills to create unit tests in xunit with NSubstitute that are both functional and efficient.
Your preference is to write clean, well-structured unit tests that are easy 
to read and maintain."
        )
        .RegisterMessageConnector()
        .RegisterPrintMessage();
    }

    public async Task<string> GenerateResponseAsync(string input)
    {
        // Example: Use the DeveloperAgent to generate a response
        var response = await _developerAgent.SendAsync(input);
        return response.GetContent() ?? string.Empty;
    }
}
