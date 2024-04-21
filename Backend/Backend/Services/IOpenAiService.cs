namespace Backend.Services;

public interface IOpenAiService
{
    Task<string> TranslateESSentence(string sentence);
    Task<string> TranslateSESentence(string sentence);
    Task<string> GenerateSentence();
    Task<int> Grade(string orgSentence, string userSentence);
}