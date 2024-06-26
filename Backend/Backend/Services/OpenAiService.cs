﻿using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OpenAI_API.Models;
using System.ComponentModel;
using System.Net.Sockets;
using System.IO;
using System.Reflection.Emit;
using System.Speech.Synthesis;

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

    public async Task<string> GenerateSentence()
    {
        var api = new OpenAI_API.OpenAIAPI(_openAiConfig.Key);
        var chat = api.Chat.CreateConversation();
        chat.AppendSystemMessage("Generate one sentence for the student to translate and nothing else.");
        var senOnEng = await chat.GetResponseFromChatbotAsync();
        var senOnSrb = await TranslateESSentence(senOnEng);
        return senOnEng + senOnSrb;
    }
    public async Task<List<string>> GetSentencesFromFile()
    {
        Random rnd = new Random();
        //string[] lines = File.ReadAllLines(@"D:\eestec-chg-naf\model\datasets\CEFR-SP_Wikiauto_train.txt");
        string[] lines = File.ReadAllLines(@"../../model/datasets/CEFR-SP_Wikiauto_train.txt");
        List<string> sentences = new List<string>();
        while (sentences.Count < 3)
        {
            // uzima iz svakog nivoa znanja po jednu recenicu i vraca ih (nivoi od 1-6)
            string line = lines[rnd.Next(1, 5990)];
            string[] split = line.Split('\t');
            if (Convert.ToInt32(split[1]) == sentences.Count + 3)
            {
                sentences.Add(split[0] + "|" + await TranslateESSentence(split[0]));
            }
        }
        return sentences;
    }
    public async Task<int> Grade(string orgSentence, string userSentence)
    {
        int sum = 0;
        int n = 3;
        for (int i = 0; i < 3; i++)
        {
            var api = new OpenAI_API.OpenAIAPI(_openAiConfig.Key);
            var chat = api.Chat.CreateConversation();
            chat.AppendSystemMessage("You are given two sentences orgSentence and userSentence grade how similar they are on the scale of 0 to 100, don't grade to softly. Only return the number of points.");
            chat.AppendUserInput(orgSentence);
            chat.AppendUserInput(userSentence);
            var res = await chat.GetResponseFromChatbotAsync();
            sum += Int32.Parse(res);
        }
        return sum / n;
    }

    public async Task<List<string>> GenerateTest()
    {
        var api = new OpenAI_API.OpenAIAPI(_openAiConfig.Key);
        List<string> lista = new List<string>();
        var chat = api.Chat.CreateConversation();
        int i = 0;
        int level = 1;
        while (i < 3)
        {
            chat.AppendSystemMessage($"Generate one sentence on English and its difficulty should be of {level} out of 5");
            var res = await chat.GetResponseFromChatbotAsync();
            lista.Add(res);
            level += 2;
            i++;
        }
        return lista;
    }

    public async Task<List<string>> GeneratePartialTest(int level)
    {
        var api = new OpenAI_API.OpenAIAPI(_openAiConfig.Key);
        List<string> lista = new List<string>();
        var chat = api.Chat.CreateConversation();
        chat.AppendSystemMessage($"Generate one sentence on English and its difficulty should be of {level} out of 5");
        int i = 0;
        while (i < 5)
        {
            var res = await chat.GetResponseFromChatbotAsync();
            lista.Add(res);
            res = await TranslateESSentence(res);
            lista.Add(res);
            i++;
        }
        return lista;
    }


    public async Task<string> GeneratePopQuiz(int level)
    {
        var api = new OpenAI_API.OpenAIAPI(_openAiConfig.Key);
        var chat = api.Chat.CreateConversation();
        chat.AppendSystemMessage($"Generate one sentence on English and its difficulty should be of {level} out of 5");
        var senOnEng = await chat.GetResponseFromChatbotAsync();
        return senOnEng;
    }

    public async Task<string> CheckPopQuiz(int level, string answer)
    {
        var api = new OpenAI_API.OpenAIAPI(_openAiConfig.Key);
        var chat = api.Chat.CreateConversation();
        chat.AppendSystemMessage($"The level {level} out of 5 represents the level of difficulty for the Pop Quiz for english sentence that user tried to translate" +
                                 $", you need to return me an array of objects where first attribute of the object is the word of the answer i will shortly provide and " +
                                 $"the second attribute is true if the word is correct and false if it isn't, and then last element of the array should be a translated version to english" +
                                 $"here is the answer: {answer}");
        var result = await chat.GetResponseFromChatbotAsync();
        return result;
    }

    public async void TTS(string sentence)
    {
        var synth = new SpeechSynthesizer();
        synth.SetOutputToDefaultAudioDevice();
        synth.Speak(sentence);
    }
}
