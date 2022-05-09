using AutoMapper;
using InteractiveHelper.Api.Controllers.Characteristics.Models;
using InteractiveHelper.CharactetisricService;
using InteractiveHelper.CharactetisricService.Models;
using Microsoft.AspNetCore.Mvc;

namespace InteractiveHelper.Api.Controllers.Characteristics;

[Route("api/v{version:apiVersion}/")]
[ApiController]
public class CharacteristicsController : ControllerBase
{
    private readonly IMapper mapper;
    private readonly ICharacteristicService characteristicService;

    public CharacteristicsController(IMapper mapper, ICharacteristicService characteristicService)
    {
        this.mapper = mapper;
        this.characteristicService = characteristicService;
    }

    [HttpGet("categories/{id}/characteristics")]
    public async Task<IEnumerable<CharacteristicResponse>> GetCategoryCharacteristics([FromRoute] int id)
    {
        return mapper.Map<IEnumerable<CharacteristicResponse>>(
            await characteristicService.GetCategoryCharacteristics(id));
    }

    [HttpGet("items/{id}/characteristics")]
    public async Task<IEnumerable<ItemCharacteristicResponse>> GetItemCharacteristics([FromRoute] int id)
    {
        return mapper.Map<IEnumerable<ItemCharacteristicResponse>>(
            await characteristicService.GetItemCharacteristics(id));
    }

    [HttpPut("categories/{id}/characteristics")]
    public async Task<IActionResult> UpdateCategoryCharacterisrics([FromRoute] int id, 
        [FromBody] IEnumerable<UpdateCharacteristicRequest> characteristicModels)
    {
        await characteristicService.UpdateCategoryCharacterisrics(id, 
            mapper.Map<IEnumerable<UpdateCharacteristicModel>>(characteristicModels));

        return Ok();
    }

    [HttpPut("items/{id}/characteristics")]
    public async Task<IActionResult> UpdateItemCharacteristics([FromRoute] int id, 
        [FromBody] IEnumerable<UpdateItemCharacteristicRequest> itemCharacteristicModels)
    {
        await characteristicService.UpdateItemCharacteristics(id,
            mapper.Map<IEnumerable<UpdateItemCharacteristicModel>>(itemCharacteristicModels));

        return Ok();
    }
}
