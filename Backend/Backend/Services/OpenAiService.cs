using Microsoft.AspNetCore.Http.HttpResults;
using OpenAI_API.Models;

namespace Backend.Services;

public class OpenAiService : IOpenAiService
{
    private readonly OpenAiConfig _openAiConfig;

    public OpenAiService(IOptionsMonitor<OpenAiConfig> optionsMonitor)
    {
        _openAiConfig = optionsMonitor.CurrentValue;
    }

    public async Task<string> CompleteSentence(string text)
    {
        //API instance
        var api = new OpenAI_API.OpenAIAPI(_openAiConfig.Key);
        var res = await api.Completions.GetCompletion(text);
        return res;
    }

    public async Task<string> CompleteSentenceAdvance(string text)
    {
        var api = new OpenAI_API.OpenAIAPI(_openAiConfig.Key);
        var res = await api.Completions.CreateCompletionAsync(new OpenAI_API.Completions.CompletionRequest(text, model: Model.CurieText, temperature: 0.1));
        return res.Completions[0].Text;
    }

    public async Task<string> CheckPLanguage(string language)
    {
        var api = new OpenAI_API.OpenAIAPI(_openAiConfig.Key);
        var chat = api.Chat.CreateConversation();
        chat.AppendSystemMessage("You are a teacher who helps programmers undestrand things are programming language or not. If user tells you programming language responds with yes, if a user tells you something which is not programming language respond with no, you will respond with yes or no.");
        chat.AppendUserInput(language);
        var res = await chat.GetResponseFromChatbotAsync();
        return res;
    }
}
