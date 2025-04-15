namespace FirstStepApp.API.Services;
public interface IOllamaService
{
    Task<string> GenerateResponseAsync(string input);
}