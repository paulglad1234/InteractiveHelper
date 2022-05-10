using AutoMapper;
using InteractiveHelper.Api.Controllers.QuizConstruction.Quizes.Models;
using InteractiveHelper.Common.Security;
using InteractiveHelper.QuizConstructionServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InteractiveHelper.Api.Controllers.QuizConstruction.Quizes;

[Route("api/v{version:apiVersion}/quizconstruction/quizes")]
[ApiController]
[ApiVersion("1.0")]
// TODO: Role-based authorization
[Authorize(AppScopes.Read)]
[Authorize(AppScopes.Write)]
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
    public async Task<QuizResponse> CreateNewQuiz()
    {
        return mapper.Map<QuizResponse>(await quizService.CreateNewQuiz());
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> SetActiveQuiz([FromQuery] int id)
    {
        await quizService.SetActiveQuiz(id);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveQuiz([FromQuery] int id)
    {
        await quizService.RemoveQuiz(id);
        return Ok();
    }
}
