

using FirstStepApp.API.Infrastructure;

namespace FirstStepApp.API.Services;

public class OllamaService : IOllamaService
{
    private readonly IOllama _ollamaClient;

    public OllamaService(IOllama ollamaClient)
    {
        _ollamaClient = ollamaClient;
    }

    public async Task<string> GenerateResponseAsync(string input)
    {
        var response = await _ollamaClient.GenerateResponseAsync(input);
        return response.ToString();
    }
}
