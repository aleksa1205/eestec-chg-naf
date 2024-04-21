using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OpenAI_API.Models;

namespace Backend.Services;

public class OpenAiService : IOpenAiService
{
    private readonly OpenAiConfig _openAiConfig;

    public OpenAiService(IOptionsMonitor<OpenAiConfig> optionsMonitor)
    {
        _openAiConfig = optionsMonitor.CurrentValue;
    }

    public async Task<string> TranslateESSentence(string sentence)
    {
        var api = new OpenAI_API.OpenAIAPI(_openAiConfig.Key);
        var chat = api.Chat.CreateConversation();
        chat.AppendSystemMessage("You are teacher who helps student learn new language. Translate the sentence that student gave you on English to Serbian.");
        chat.AppendUserInput(sentence);
        var res = await chat.GetResponseFromChatbotAsync();
        return res;
    }

    public async Task<string> TranslateSESentence(string sentence)
    {
        var api = new OpenAI_API.OpenAIAPI(_openAiConfig.Key);
        var chat = api.Chat.CreateConversation();
        chat.AppendSystemMessage("You are teacher who helps student learn new language. Translate the sentence that student gave you on Serbian to English.");
        chat.AppendUserInput(sentence);
        var res = await chat.GetResponseFromChatbotAsync();
        return res;
    }

    public async Task<string> GenerateSentence(){
        var api = new OpenAI_API.OpenAIAPI(_openAiConfig.Key);
        var chat = api.Chat.CreateConversation();
        chat.AppendSystemMessage("Generate one sentence for the student to translate and nothing else.");
        var senOnEng = await chat.GetResponseFromChatbotAsync();
        var senOnSrb = await TranslateESSentence(senOnEng);
        return senOnEng + senOnSrb;
    }

    public async Task<int> Grade(string orgSentence, string userSentence)
    {
        int sum = 0;
        for(int i = 0; i < 3; i++)
        {
            var api = new OpenAI_API.OpenAIAPI(_openAiConfig.Key);
            var chat = api.Chat.CreateConversation();
            chat.AppendSystemMessage("You are given two sentences orgSentence and userSentence grade how similar they are on the scale of 0 to 100, don't grade to softly. Only return the number of points.");
            chat.AppendUserInput(orgSentence);
            chat.AppendUserInput(userSentence);
            var res= await chat.GetResponseFromChatbotAsync();
            sum += Int32.Parse(res);
        }
        return sum/3;
    }
}
