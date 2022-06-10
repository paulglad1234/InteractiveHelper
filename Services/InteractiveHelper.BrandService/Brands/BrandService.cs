using AutoMapper;
using InteractiveHelper.CatalogServices.Brands.Models;
using InteractiveHelper.Common.Exceptions;
using InteractiveHelper.Db.Context;
using InteractiveHelper.Db.Entities.Catalog;
using Microsoft.EntityFrameworkCore;

namespace InteractiveHelper.CatalogServices.Brands;

internal class BrandService : IBrandService
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IMapper mapper;

    public BrandService(IDbContextFactory<MainDbContext> dbContextFactory, IMapper mapper)
    {
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<BrandModel>> GetBrands()
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var brands = context.Brands.AsQueryable();

        return await brands.Select(brand => mapper.Map<BrandModel>(brand)).ToListAsync();
    }

    public async Task<BrandModel> AddBrand(AddBrandModel model)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var brand = mapper.Map<Brand>(model);
        await context.Brands.AddAsync(brand);
        await context.SaveChangesAsync();

        return mapper.Map<BrandModel>(brand);
    }

    public async Task<BrandModel> UpdateBrand(int brandId, UpdateBrandModel model)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var brand = await GetBrandByIdFromContext(context, brandId);

        brand = mapper.Map(model, brand);

        context.Brands.Update(brand);
        context.SaveChanges();

        return mapper.Map<BrandModel>(brand);
    }

    public async Task DeleteBrand(int brandId)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var brand = await GetBrandByIdFromContext(context, brandId);

        context.Remove(brand);
        context.SaveChanges();
    }

    private static async Task<Brand> GetBrandByIdFromContext(MainDbContext context, int brandId)
    {
        return await context.Brands.FindAsync(brandId)
            ?? throw new CommonException(404, $"The brand with id:{brandId} was not found");
    }

    public async Task<IEnumerable<ItemModel>> GetBrandsItems(int brandId, int offset = 0, int limit = 10)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var brand = await GetBrandByIdFromContext(context, brandId);

        var items = brand.Items.AsQueryable()
            .Skip(Math.Max(offset, 0))
            .Take(Math.Max(0, Math.Min(limit, 1000)));

        return await items.Select(item => mapper.Map<ItemModel>(item)).ToListAsync();
    }
}
