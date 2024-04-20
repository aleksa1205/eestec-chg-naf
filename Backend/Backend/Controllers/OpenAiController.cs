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

    [HttpPost]
    public async Task<IActionResult> CompleteSentence(string text)
    {
        var result = await _openAiService.CompleteSentenceAdvance(text);
        return Ok(result);
    }

}
