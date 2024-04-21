using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class OpenAiController : ControllerBase
{
    private readonly ILogger<OpenAiController> _logger;
    private readonly IOpenAiService _openAiService;

    public OpenAiController(ILogger<OpenAiController> logger, IOpenAiService openAiService)
    {
        _logger = logger;
        _openAiService = openAiService;
    }

    [HttpGet("Translate-English-Serbian/{sentence}")]
    public async Task<IActionResult> TranslateEnglish(string sentence)
    {
        var res = await _openAiService.TranslateESSentence(sentence);
        return Ok(res);
    }

    [HttpGet("Translate-Serbian-English/{sentence}")]
    public async Task<IActionResult> TranslateSerian(string sentence)
    {
        var res = await _openAiService.TranslateSESentence(sentence);
        return Ok(res);
    }

    [HttpGet("Generate")]
    public async Task<IActionResult> Generate()
    {
        var res = await _openAiService.GenerateSentence();
        return Ok(res);
    }

    [HttpGet("Grade/{originalSentence}/{userSentence}")]
    public async Task<IActionResult> Grade(string originalSentence, string userSentence)
    {
        var res = await _openAiService.Grade(originalSentence, userSentence);
        return Ok(res);
    }

    [HttpGet("GenerateTest")]
    public async Task<IActionResult> GenerateTest()
    {
        var res= await _openAiService.GenerateTest();
        return Ok(res);
    }

    [HttpGet("GeneratePartialTest/{level}")]
    public async Task<IActionResult> GeneratePartialTest(int level)
    {
        var res = await _openAiService.GeneratePartialTest(level);
        return Ok(res);
    }
}
