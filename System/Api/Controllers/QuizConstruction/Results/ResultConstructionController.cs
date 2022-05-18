using AutoMapper;
using InteractiveHelper.Api.Controllers.QuizConstruction.Results.Models;
using InteractiveHelper.Common.Security;
using InteractiveHelper.QuizConstructionServices;
using InteractiveHelper.QuizConstructionServices.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InteractiveHelper.Api.Controllers.QuizConstruction.Results;

[Route("api/v{version:apiVersion}/quizconstruction/results")]
[ApiController]
[ApiVersion("1.0")]
[Authorize(AppScopes.AdminQuiz)]
public class ResultConstructionController : Controller
{
    private readonly IMapper mapper;
    private readonly IResultConstructionService resultService;

    public ResultConstructionController(IMapper mapper, IResultConstructionService resultService)
    {
        this.mapper = mapper;
        this.resultService = resultService;
    }

    [HttpGet("of/{quizId}")]
    public async Task<IEnumerable<ResultResponse>> GetQuizResults([FromQuery] int quizId)
    {
        return mapper.Map<IEnumerable<ResultResponse>>(await resultService.GetQuizResults(quizId));
    }

    [HttpPost("addTo/{quizId}")]
    public async Task<ResultResponse> AddResultToQuiz([FromQuery] int quizId, [FromBody] AddResultRequest addResultRequest)
    {
        return mapper.Map<ResultResponse>(
            await resultService.AddResultToQuiz(quizId,
            mapper.Map<AddResultModel>(addResultRequest)));
    }

    [HttpPut("{id}/addItem/{itemId}")]
    public async Task<ResultResponse> AddItemToResult([FromQuery] int id, [FromQuery] int itemId)
    {
        return mapper.Map<ResultResponse>(
            await resultService.AddItemToResult(id, itemId));
    }

    [HttpPut("{id}/removeItem/{itemId}")]
    public async Task<ResultResponse> RemoveItemFromResult([FromQuery] int id, [FromQuery] int itemId)
    {
        return mapper.Map<ResultResponse>(
            await resultService.AddItemToResult(id, itemId));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateResult([FromQuery] int id, [FromBody] UpdateResultRequest updateResultRequest)
    {
        await resultService.UpdateResult(id, mapper.Map<UpdateResultModel>(updateResultRequest));
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveResult([FromQuery] int id)
    {
        await resultService.RemoveResult(id);
        return Ok();
    }
}
