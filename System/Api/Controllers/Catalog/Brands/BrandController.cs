using AutoMapper;
using InteractiveHelper.Api.Controllers.Catalog.Brands.Models;
using InteractiveHelper.CatalogServices.Brands;
using InteractiveHelper.CatalogServices.Brands.Models;
using InteractiveHelper.Common.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InteractiveHelper.Api.Controllers.Catalog.Brands;

/// <summary>
/// Contains CRUD actions for brands in catalog
/// </summary>
[Route("api/v{version:apiVersion}/brands")]
[ApiController]
[ApiVersion("1.0")]
[Area("Catalog")]
[Authorize(AppScopes.AdminCatalog)]
public class BrandController : Controller
{
    private readonly IMapper mapper;
    private readonly IBrandService brandService;

    public BrandController(IMapper mapper, IBrandService brandService)
    {
        this.mapper = mapper;
        this.brandService = brandService;
    }

    /// <summary>
    /// Returns all brands that exist in catalog
    /// </summary>
    /// <returns>Collection of brands</returns>
    [HttpGet("")]
    [AllowAnonymous]
    public async Task<IEnumerable<BrandResponse>> GetBrands()
    {
        var brands = await brandService.GetBrands();

        return mapper.Map<IEnumerable<BrandResponse>>(brands);
    }

    /// <summary>
    /// Returns the given amount of brand's items from catalog
    /// </summary>
    /// <param name="id">Brand id</param>
    /// <param name="offset"></param>
    /// <param name="limit"></param>
    /// <returns>Collection of items</returns>
    [HttpGet("{id}/items")]
    [AllowAnonymous]
    public async Task<IEnumerable<BrandsItemResponse>> GetBrandsItems([FromRoute] int id,
        [FromQuery] int offset = 0, [FromQuery] int limit = 10)
    {
        var items = await brandService.GetBrandsItems(id, offset, limit);
        return mapper.Map<IEnumerable<BrandsItemResponse>>(items);
    }

    /// <summary>
    /// Creates new brand with properties given in the request body
    /// </summary>
    /// <param name="request">Brand properties</param>
    /// <returns>The newly created brand</returns>
    [HttpPost("")]
    public async Task<BrandResponse> AddBrand([FromBody] AddBrandRequest request)
    {
        var model = mapper.Map<AddBrandModel>(request);
        var brand = await brandService.AddBrand(model);
        return mapper.Map<BrandResponse>(brand);
    }

    /// <summary>
    /// Updates a brand with properties given in the request body
    /// </summary>
    /// <param name="id">Brand id</param>
    /// <param name="request">Brand properties</param>
    /// <returns>Updated brand</returns>
    [HttpPut("{id}")]
    public async Task<BrandResponse> UpdateBrand([FromRoute] int id, [FromBody] UpdateBrandRequest request)
    {
        var model = mapper.Map<UpdateBrandModel>(request);
        var brand = await brandService.UpdateBrand(id, model);

        return mapper.Map<BrandResponse>(brand);
    }

    /// <summary>
    /// Deletes a brand with given id
    /// </summary>
    /// <param name="id">Brand id</param>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBrand([FromRoute] int id)
    {
        await brandService.DeleteBrand(id);

        return Ok();
    }
}
