using AutoMapper;
using InteractiveHelper.Api.Controllers.Brands.Models;
using InteractiveHelper.BrandService;
using InteractiveHelper.BrandService.Models;
using InteractiveHelper.Common.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InteractiveHelper.Api.Controllers.Brands;

[Route("api/v{version:apiVersion}/brands")]
[ApiController]
[ApiVersion("1.0")]
public class BrandController : Controller
{
    private readonly IMapper mapper;
    private readonly IBrandService brandService;

    public BrandController(IMapper mapper, IBrandService brandService)
    {
        this.mapper = mapper;
        this.brandService = brandService;
    }

    [HttpGet("")]
    public async Task<IEnumerable<BrandResponse>> GetBrands()
    {
        var brands = await brandService.GetBrands();

        return mapper.Map<IEnumerable<BrandResponse>>(brands);
    }

    [HttpGet("{id}/items")]
    public async Task<IEnumerable<BrandsItemResponse>> GetBrandsItems([FromRoute] int id)
    {
        var items = await brandService.GetBrandsItems(id);
        return mapper.Map<IEnumerable<BrandsItemResponse>>(items);
    }

    [HttpPost("")]
    [Authorize(AppScopes.Write)]
    public async Task<BrandResponse> AddBrand([FromBody] AddBrandRequest request)
    {
        var model = mapper.Map<AddBrandModel>(request);
        var brand = await brandService.AddBrand(model);

        return mapper.Map<BrandResponse>(brand);
    }

    [HttpPut("{id}")]
    [Authorize(AppScopes.Write)]
    public async Task<BrandResponse> UpdateBrand([FromRoute] int id, [FromBody] UpdateBrandRequest request)
    {
        var model = mapper.Map<UpdateBrandModel>(request);
        var brand = await brandService.UpdateBrand(id, model);

        return mapper.Map<BrandResponse>(brand);
    }

    [HttpDelete("{id}")]
    [Authorize(AppScopes.Write)]
    public async Task<IActionResult> DeleteBrand([FromRoute] int id)
    {
        await brandService.DeleteBrand(id);

        return Ok();
    }
}
