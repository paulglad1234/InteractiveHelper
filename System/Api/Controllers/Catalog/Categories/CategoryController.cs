using AutoMapper;
using InteractiveHelper.Api.Controllers.Catalog.Categories.Models;
using InteractiveHelper.CatalogServices.Categories;
using InteractiveHelper.CatalogServices.Categories.Models;
using InteractiveHelper.Common.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InteractiveHelper.Api.Controllers.Categories;

/// <summary>
/// Contains CRUD actions for categories in catalog
/// </summary>
[Route("api/v{version:apiVersion}/categories")]
[ApiController]
[ApiVersion("1.0")]
[Area("Catalog")]
[Authorize(AppScopes.AdminCatalog)]
public class CategoryController : Controller
{
    private readonly IMapper mapper;
    private readonly ICategoryService categoryService;

    public CategoryController(IMapper mapper, ICategoryService categoryService)
    {
        this.mapper = mapper;
        this.categoryService = categoryService;
    }

    /// <summary>
    /// Returns all categories that exist in catalog
    /// </summary>
    /// <returns>Collection of categories</returns>
    [HttpGet("")]
    [AllowAnonymous]
    public async Task<IEnumerable<CategoryResponse>> GetCategories()
    {
        var categories = await categoryService.GetCategories();

        return mapper.Map<IEnumerable<CategoryResponse>>(categories);
    }

    /// <summary>
    /// Returns the given amount of category's items from catalog
    /// </summary>
    /// <param name="id">Category id</param>
    /// <param name="offset"></param>
    /// <param name="limit"></param>
    /// <returns>Collection of items</returns>
    [HttpGet("{id}/items")]
    [AllowAnonymous]
    public async Task<IEnumerable<CategoryItemResponse>> GetCategoryItems([FromRoute] int id,
        [FromQuery] int offset = 0, [FromQuery] int limit = 10)
    {
        var items = await categoryService.GetCategoryItems(id, offset, limit);
        return mapper.Map<IEnumerable<CategoryItemResponse>>(items);
    }

    /// <summary>
    /// Creates new category with properties given in the request body
    /// </summary>
    /// <param name="request">Category properties</param>
    /// <returns>The newly created category</returns>
    [HttpPost("")]
    public async Task<CategoryResponse> AddCategory([FromBody] AddCategoryRequest request)
    {
        var model = mapper.Map<AddCategoryModel>(request);
        var category = await categoryService.AddCategory(model);

        return mapper.Map<CategoryResponse>(category);
    }

    /// <summary>
    /// Updates a category with properties given in the request body
    /// </summary>
    /// <param name="id">Category id</param>
    /// <param name="request">Category properties</param>
    /// <returns>The updated category</returns>
    [HttpPut("{id}")]
    public async Task<CategoryResponse> UpdateCategory([FromRoute] int id, [FromBody] UpdateCategoryRequest request)
    {
        var model = mapper.Map<UpdateCategoryModel>(request);
        var category = await categoryService.UpdateCategory(id, model);

        return mapper.Map<CategoryResponse>(category);
    }

    /// <summary>
    /// Deletes a category with given id
    /// </summary>
    /// <param name="id">Category id</param>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory([FromRoute] int id)
    {
        await categoryService.DeleteCategory(id);

        return Ok();
    }
}
