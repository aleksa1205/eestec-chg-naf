using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;

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
        try
        {
            var res = await _openAiService.TranslateESSentence(sentence);
            return Ok(res);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("Translate-Serbian-English/{sentence}")]
    public async Task<IActionResult> TranslateSerian(string sentence)
    {
        try
        {
            var res = await _openAiService.TranslateSESentence(sentence);
            return Ok(res);
        }
        catch(Exception ex){
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("Generate")]
    public async Task<IActionResult> Generate()
    {
        try
        {
            var res = await _openAiService.GenerateSentence();
            return Ok(res);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("GetSentencesFromFile")]
    public async Task<IActionResult> GetSentencesFromFile()
    {
        try
        {
            var res = await _openAiService.GetSentencesFromFile();
            return Ok(res);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("Grade/{originalSentence}/{userSentence}")]
    public async Task<IActionResult> Grade(string originalSentence, string userSentence)
    {
        try
        {
            var res = await _openAiService.Grade(originalSentence, userSentence);
            return Ok(res);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("GenerateTest")]
    public async Task<IActionResult> GenerateTest()
    {
        try
        {
            var res = await _openAiService.GenerateTest();
            return Ok(res);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("GeneratePartialTest/{level}")]
    public async Task<IActionResult> GeneratePartialTest(int level)
    {
        try
        {
            var res = await _openAiService.GeneratePartialTest(level);
            return Ok(res);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /*
    [HttpGet("aaa/{a}")]
    public async Task<IActionResult> g(string a)
    {
        await _openAiService.G(a);
        return Ok();
    }
    */

    [HttpGet("GeneratePopQuiz/{level}")]
    public async Task<IActionResult> GeneratePopQuiz(int level)
    {
        try
        {
            var res = await _openAiService.GeneratePopQuiz(level);
            return Ok(res);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("CheckPopQuiz/{level}/{answer}")]
    public async Task<IActionResult> CheckPopQuiz(int level, string answer)
    {
        try
        {
            var res = await _openAiService.CheckPopQuiz(level, answer);
            return Ok(res);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("TTS")]
    public async Task<IActionResult> TTS(string sentence)
    {
        try
        {
            _openAiService.TTS(sentence);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
