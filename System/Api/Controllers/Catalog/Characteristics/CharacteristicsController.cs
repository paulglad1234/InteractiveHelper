using AutoMapper;
using InteractiveHelper.Api.Controllers.Catalog.Characteristics.Models;
using InteractiveHelper.CatalogServices.Characteristics;
using InteractiveHelper.CatalogServices.Characteristics.Models;
using InteractiveHelper.Common.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InteractiveHelper.Api.Controllers.Catalog.Characteristics;

[Route("api/v{version:apiVersion}/")]
[ApiController]
[Area("Catalog")]
public class CharacteristicsController : ControllerBase
{
    private readonly IMapper mapper;
    private readonly ICharacteristicService characteristicService;

    public CharacteristicsController(IMapper mapper, ICharacteristicService characteristicService)
    {
        this.mapper = mapper;
        this.characteristicService = characteristicService;
    }

    /// <summary>
    /// Returns characteristics assigned to the category with given id
    /// </summary>
    /// <param name="id">Category id</param>
    /// <returns>Collection of characteristics</returns>
    [HttpGet("categories/{id}/characteristics")]
    public async Task<IEnumerable<CharacteristicResponse>> GetCategoryCharacteristics([FromRoute] int id)
    {
        return mapper.Map<IEnumerable<CharacteristicResponse>>(
            await characteristicService.GetCategoryCharacteristics(id));
    }

    /// <summary>
    /// Returns characteristic values of the item with given id
    /// </summary>
    /// <param name="id">Item id</param>
    /// <returns>Collection of characteristic ids and values</returns>
    [HttpGet("items/{id}/characteristics")]
    public async Task<IEnumerable<ItemCharacteristicResponse>> GetItemCharacteristics([FromRoute] int id)
    {
        return mapper.Map<IEnumerable<ItemCharacteristicResponse>>(
            await characteristicService.GetItemCharacteristics(id));
    }

    /// <summary>
    /// Assign the given list of characteristics to the category with given id
    /// </summary>
    /// <param name="id">Category id</param>
    /// <param name="characteristicModels">Collection of characteristic properties</param>
    /// <returns></returns>
    [HttpPut("categories/{id}/characteristics")]
    [Authorize(AppScopes.AdminCatalog)]
    public async Task<IActionResult> UpdateCategoryCharacterisrics([FromRoute] int id, 
        [FromBody] IEnumerable<UpdateCharacteristicRequest> characteristicModels)
    {
        await characteristicService.UpdateCategoryCharacterisrics(id, 
            mapper.Map<IEnumerable<UpdateCharacteristicModel>>(characteristicModels));

        return Ok();
    }

    /// <summary>
    /// Assign the given list of characteristic values to the item with given id
    /// </summary>
    /// <param name="id">Item id</param>
    /// <param name="itemCharacteristicModels">Collection of characteristic values</param>
    /// <returns></returns>
    /// <response code="200">If operation successful</response>
    /// <response code="400">If the characteristic list does not corespond item's category characteristics</response>
    [HttpPut("items/{id}/characteristics")]
    [Authorize(AppScopes.AdminCatalog)]
    public async Task<IActionResult> UpdateItemCharacteristics([FromRoute] int id, 
        [FromBody] IEnumerable<UpdateItemCharacteristicRequest> itemCharacteristicModels)
    {
        await characteristicService.UpdateItemCharacteristics(id,
            mapper.Map<IEnumerable<UpdateItemCharacteristicModel>>(itemCharacteristicModels));

        return Ok();
    }
}
