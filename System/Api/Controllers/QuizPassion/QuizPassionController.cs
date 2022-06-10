using AutoMapper;
using InteractiveHelper.Common.Security;
using InteractiveHelper.QuizConstructionServices.Quizes.Models;
using InteractiveHelper.QuizConstructionServices.Results.Models;
using InteractiveHelper.QuizPassionService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InteractiveHelper.Api.Controllers.QuizPassion;

[Route("api/v{version:apiVersion}/quizconstruction/quizes")]
[ApiController]
[ApiVersion("1.0")]
[Authorize(AppScopes.AdminQuiz)]
public class QuizPassionController : Controller
{
    private readonly IMapper mapper;
    private readonly IQuizPassionService quizService;

    public QuizPassionController(IMapper mapper, IQuizPassionService quizService)
    {
        this.mapper = mapper;
        this.quizService = quizService;
    }

    [HttpGet("{url}")]
    public async Task<OutputQuizModel> RootStage([FromRoute] string url)
    {
        return await quizService.RootStage(url);
    }

    [HttpGet("{answerId}")]
    public async Task<OutputNodeModel> NextStage([FromRoute] int answerId)
    {
        return await quizService.NextStage(answerId);
    }
}
