using AutoMapper;
using InteractiveHelper.Api.Controllers.Categories.Models;
using InteractiveHelper.CategoryService;
using InteractiveHelper.CategoryService.Models;
using Microsoft.AspNetCore.Mvc;

namespace InteractiveHelper.Api.Controllers.Categories;

[Route("api/v{version:apiVersion}/categories")]
[ApiController]
[ApiVersion("1.0")]
public class CategoryController : Controller
{
    private readonly IMapper mapper;
    private readonly ICategoryService categoryService;

    public CategoryController(IMapper mapper, ICategoryService categoryService)
    {
        this.mapper = mapper;
        this.categoryService = categoryService;
    }

    [HttpGet("")]
    public async Task<IEnumerable<CategoryResponse>> GetCategories()
    {
        var categories = await categoryService.GetCategories();

        return mapper.Map<IEnumerable<CategoryResponse>>(categories);
    }

    [HttpGet("{id}/items")]
    public async Task<IEnumerable<CategoryItemResponse>> GetCategoryItems([FromRoute] int id)
    {
        var items = await categoryService.GetCategoryItems(id);
        return mapper.Map<IEnumerable<CategoryItemResponse>>(items);
    }

    [HttpPost("")]
    public async Task<CategoryResponse> AddCategory([FromBody] AddCategoryRequest request)
    {
        var model = mapper.Map<AddCategoryModel>(request);
        var category = await categoryService.AddCategory(model);

        return mapper.Map<CategoryResponse>(category);
    }

    [HttpPut("{id}")]
    public async Task<CategoryResponse> UpdateCategory([FromRoute] int id, [FromBody] UpdateCategoryRequest request)
    {
        var model = mapper.Map<UpdateCategoryModel>(request);
        var category = await categoryService.UpdateCategory(id, model);

        return mapper.Map<CategoryResponse>(category);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory([FromRoute] int id)
    {
        await categoryService.DeleteCategory(id);

        return Ok();
    }
}
