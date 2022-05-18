using AutoMapper;
using InteractiveHelper.Api.Controllers.QuizConstruction.Questions.Models;
using InteractiveHelper.Common.Security;
using InteractiveHelper.QuizConstructionServices;
using InteractiveHelper.QuizConstructionServices.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InteractiveHelper.Api.Controllers.QuizConstruction.Questions;

[Route("api/v{version:apiVersion}/quizconstruction/questions")]
[ApiController]
[ApiVersion("1.0")]
[Authorize(AppScopes.AdminQuiz)]
public class QuestionConstructionController : Controller
{
    private readonly IMapper mapper;
    private readonly IQuestionConstructionService questionService;

    public QuestionConstructionController(IMapper mapper, IQuestionConstructionService questionService)
    {
        this.mapper = mapper;
        this.questionService = questionService;
    }

    [HttpGet("of/{quizId}")]
    public async Task<IEnumerable<QuestionResponse>> GetQuizQuestions([FromQuery] int quizId)
    {
        return mapper.Map<IEnumerable<QuestionResponse>>(await questionService.GetQuizQuestions(quizId));
    }

    [HttpPost("addTo/{quizId}")]
    public async Task<QuestionResponse> AddQuestionToQuiz([FromQuery] int quizId,
        [FromBody] AddQuestionRequest addQuestionRequest)
    {
        return mapper.Map<QuestionResponse>(
            await questionService.AddQuestionToQuiz(quizId,
            mapper.Map<AddQuestionModel>(addQuestionRequest)));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateQuestion([FromQuery] int id, [FromBody] UpdateQuestionRequest updateQuestionRequest)
    {
        await questionService.UpdateQuestion(id, mapper.Map<UpdateQuestionModel>(updateQuestionRequest));
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteQuestion([FromQuery] int id)
    {
        await questionService.RemoveQuestion(id);
        return Ok();
    }
}
