namespace Backend.Services;

public interface IOpenAiService
{
    Task<string> CompleteSentence(string text);
    Task<string> CompleteSentenceAdvance(string text);
    Task<string> CheckPLanguage(string language);
}