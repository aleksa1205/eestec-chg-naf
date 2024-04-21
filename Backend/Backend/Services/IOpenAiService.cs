namespace Backend.Services;

public interface IOpenAiService
{
    Task<string> TranslateESSentence(string sentence);
    Task<string> TranslateSESentence(string sentence);
    Task<string> GenerateSentence();
    Task<List<string>> GetSentencesFromFile();
    Task<int> Grade(string orgSentence, string userSentence);

    Task G(string sen);
    Task<List<string>> GenerateTest();
    Task<List<string>> GeneratePartialTest(int level);
    Task<string> GeneratePopQuiz(int level);
    Task<string> CheckPopQuiz(int level, string answer);
}