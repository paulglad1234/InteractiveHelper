using AutoMapper;
using InteractiveHelper.Api.Controllers.QuizConstruction.Answers.Models;
using InteractiveHelper.Common.Security;
using InteractiveHelper.QuizConstructionServices;
using InteractiveHelper.QuizConstructionServices.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InteractiveHelper.Api.Controllers.QuizConstruction.Answers;

[Route("api/v{version:apiVersion}/quizconstruction/answers")]
[ApiController]
[ApiVersion("1.0")]
// TODO: Role-based authorization
[Authorize(AppScopes.Read)]
[Authorize(AppScopes.Write)]
public class AnswerConstructionController : Controller
{
    private readonly IMapper mapper;
    private readonly IAnswerConstructionService answerService;

    public AnswerConstructionController(IMapper mapper, IAnswerConstructionService answerService)
    {
        this.mapper = mapper;
        this.answerService = answerService;
    }

    [HttpGet("to:{questionId}")]
    public async Task<IEnumerable<AnswerResponse>> GetQuestionAnswers([FromQuery] int questionId)
    {
        return mapper.Map<IEnumerable<AnswerResponse>>(await answerService.GetQuestionAnswers(questionId));
    }

    [HttpPost("addTo/{questionId}")]
    public async Task<AnswerResponse> AddNewAnswerToQuestion([FromQuery] int questionId,
        [FromBody] AddAnswerRequest addAnswerRequest)
    {
        return mapper.Map<AnswerResponse>(
            await answerService.AddNewAnswerToQuestion(questionId,
            mapper.Map<AddAnswerModel>(addAnswerRequest)));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAnswer([FromQuery] int id, [FromBody] UpdateAnswerRequest updateAnswerRequest)
    {
        await answerService.UpdateAnswer(id, mapper.Map<UpdateAnswerModel>(updateAnswerRequest));
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAnswer([FromQuery] int id)
    {
        await answerService.RemoveAnswer(id);
        return Ok();
    }
}
