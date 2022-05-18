using AutoMapper;
using InteractiveHelper.Api.Controllers.Catalog.Items.Models;
using InteractiveHelper.Common.Security;
using InteractiveHelper.ItemService;
using InteractiveHelper.ItemService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InteractiveHelper.Api.Controllers.Catalog.Items;

/// <summary>
/// Contains CRUD actions for items in catalog
/// </summary>
[Route("api/v{version:apiVersion}/items")]
[ApiController]
[ApiVersion("1.0")]
[Area("Catalog")]
[Authorize(AppScopes.AdminCatalog)]
public class ItemController : ControllerBase
{
    private readonly IMapper mapper;
    private readonly IItemService itemService;

    public ItemController(IMapper mapper, IItemService itemService)
    {
        this.mapper = mapper;
        this.itemService = itemService;
    }

    /// <summary>
    /// Returns the given amount of items from catalog
    /// </summary>
    /// <param name="offset"></param>
    /// <param name="limit"></param>
    /// <returns>Collection of items</returns>
    [HttpGet("")]
    [AllowAnonymous]
    public async Task<IEnumerable<ItemResponse>> GetItems([FromQuery] int offset = 0, [FromQuery] int limit = 10)
    {
        var items = await itemService.GetItems(offset, limit);

        return mapper.Map<IEnumerable<ItemResponse>>(items);
    }

    /// <summary>
    /// Returns the item with given id
    /// </summary>
    /// <param name="id">Item id</param>
    /// <returns>Item</returns>
    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<ItemResponse> GetItemById([FromRoute] int id)
    {
        var item = await itemService.GetItem(id);

        return mapper.Map<ItemResponse>(item);
    }

    /// <summary>
    /// Creates new item with properties given in the request body
    /// </summary>
    /// <param name="request">Item properties</param>
    /// <returns>Newly created item</returns>
    [HttpPost("")]
    public async Task<ItemResponse> AddItem([FromBody] AddItemRequest request)
    {
        var model = mapper.Map<AddItemModel>(request);
        var item = await itemService.AddItem(model);

        return mapper.Map<ItemResponse>(item);
    }

    /// <summary>
    /// Updates the item with given id with properties given in the request body
    /// </summary>
    /// <param name="id">Item id</param>
    /// <param name="request">Item properties</param>
    /// <returns>The updated item</returns>
    [HttpPut("{id}")]
    public async Task<ItemResponse> UpdateItem([FromRoute] int id, [FromBody] UpdateItemRequest request)
    {
        var model = mapper.Map<UpdateItemModel>(request);
        var item = await itemService.UpdateItem(id, model);

        return mapper.Map<ItemResponse>(item);
    }

    /// <summary>
    /// Deletes item with given id
    /// </summary>
    /// <param name="id">Item id</param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteItem([FromRoute] int id)
    {
        await itemService.DeleteItem(id);

        return Ok();
    }
}
