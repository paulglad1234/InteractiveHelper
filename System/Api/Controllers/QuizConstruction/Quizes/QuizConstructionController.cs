using AutoMapper;
using InteractiveHelper.Api.Controllers.QuizConstruction.Quizes.Models;
using InteractiveHelper.Common.Security;
using InteractiveHelper.QuizConstructionServices.Quizes;
using InteractiveHelper.QuizConstructionServices.Quizes.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InteractiveHelper.Api.Controllers.QuizConstruction.Quizes;

[Route("api/v{version:apiVersion}/quizconstruction/quizes")]
[ApiController]
[ApiVersion("1.0")]
[Authorize(AppScopes.AdminQuiz)]
public class QuizConstructionController : Controller
{
    private readonly IMapper mapper;
    private readonly IQuizConstructionService quizService;

    public QuizConstructionController(IMapper mapper, IQuizConstructionService quizService)
    {
        this.mapper = mapper;
        this.quizService = quizService;
    }

    [HttpGet("")]
    public async Task<IEnumerable<QuizResponse>> GetAllQuizes()
    {
        return mapper.Map<IEnumerable<QuizResponse>>(await quizService.GetAllQuizes());
    }

    [HttpPost("")]
    public async Task<QuizResponse> CreateNewQuiz([FromBody] QuizRequest request)
    {
        return mapper.Map<QuizResponse>(await quizService.CreateNewQuiz(mapper.Map<InputQuizModel>(request)));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveQuiz([FromQuery] int id)
    {
        await quizService.RemoveQuiz(id);
        return Ok();
    }
}
