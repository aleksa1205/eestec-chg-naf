namespace Backend.Configuration;

public class OpenAiConfig
{
    public string? Key { get; set; } = System.Environment.GetEnvironmentVariable("OpenAI", EnvironmentVariableTarget.Machine);
}
