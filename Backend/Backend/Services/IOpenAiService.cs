namespace Backend.Services;

public interface IOpenAiService
{
    Task<string> TranslateESSentence(string sentence);
    Task<string> TranslateSESentence(string sentence);
    Task<string> GenerateSentence();
    Task<string[]> GetSentencesFromFile();
    Task<int> Grade(string orgSentence, string userSentence);
    Task<List<string>> GenerateTest();
    Task<List<string>> GeneratePartialTest(int level);
}