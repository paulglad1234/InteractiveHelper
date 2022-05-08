using AutoMapper;
using InteractiveHelper.Api.Controllers.Items.Models;
using InteractiveHelper.ItemService;
using InteractiveHelper.ItemService.Models;
using Microsoft.AspNetCore.Mvc;

namespace InteractiveHelper.Api.Controllers.Items;

[Route("api/v{version:apiVersion}/items")]
[ApiController]
[ApiVersion("1.0")]
public class ItemController : ControllerBase
{
    private readonly IMapper mapper;
    private readonly IItemService itemService;

    public ItemController(IMapper mapper, IItemService itemService)
    {
        this.mapper = mapper;
        this.itemService = itemService;
    }

    [HttpGet("")]
    public async Task<IEnumerable<ItemResponse>> GetItems([FromQuery] int offset = 0, [FromQuery] int limit = 10)
    {
        var items = await itemService.GetItems(offset, limit);

        return mapper.Map<IEnumerable<ItemResponse>>(items);
    }

    [HttpGet("{id}")]
    public async Task<ItemResponse> GetItemById([FromRoute] int id)
    {
        var item = await itemService.GetItem(id);

        return mapper.Map<ItemResponse>(item);
    }

    [HttpPost("")]
    public async Task<ItemResponse> AddItem([FromBody] AddItemRequest request)
    {
        var model = mapper.Map<AddItemModel>(request);
        var item = await itemService.AddItem(model);

        return mapper.Map<ItemResponse>(item);
    }

    [HttpPut("{id}")]
    public async Task<ItemResponse> UpdateItem([FromRoute] int id, [FromBody] UpdateItemRequest request)
    {
        var model = mapper.Map<UpdateItemModel>(request);
        var item = await itemService.UpdateItem(id, model);

        return mapper.Map<ItemResponse>(item);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteItem([FromRoute] int id)
    {
        await itemService.DeleteItem(id);

        return Ok();
    }
}
