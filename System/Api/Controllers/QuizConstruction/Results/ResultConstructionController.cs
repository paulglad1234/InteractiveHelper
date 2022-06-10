using AutoMapper;
using InteractiveHelper.CatalogServices.Items.Models;
using InteractiveHelper.Common.Security;
using InteractiveHelper.QuizConstructionServices;
using InteractiveHelper.QuizConstructionServices.Results.Models;
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

    [HttpGet("{quizId}/tree")]
    public async Task<OutputNodeModel> ResultTree([FromRoute] int quizId)
    {
        return await resultService.RegrowResultTreeForQuiz(quizId);
    }

    [HttpGet("itemListOf/{leafId}")]
    public async Task<IEnumerable<ItemModel>> GetLeafItems(int leafId)
    {
        return await resultService.GetLeafItems(leafId);
    }

    [HttpPost("addItemTo/{leafId}")]
    public async Task<OutputNodeModel> AddItemToLeaf([FromQuery] int itemId, [FromRoute] int leafId)
    {
        return await resultService.AddItemToLeaf(itemId, leafId);
    }

    [HttpDelete("removeItemFrom/{leafId}")]
    public async Task<OutputNodeModel> RemoveItemFromLeaf([FromQuery] int itemId, [FromRoute] int leafId)
    {
        return await resultService.RemoveItemFromLeaf(itemId, leafId);
    }
}
