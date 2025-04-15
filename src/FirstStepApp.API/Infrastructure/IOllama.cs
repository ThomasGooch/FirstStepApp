namespace FirstStepApp.API.Infrastructure;
public interface IOllama
{
    Task<string> GenerateResponseAsync(string input);
}