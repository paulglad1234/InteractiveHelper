using AutoMapper;
using InteractiveHelper.CategoryService.Models;
using InteractiveHelper.Common.Exceptions;
using InteractiveHelper.Db.Context;
using InteractiveHelper.Db.Entities.Catalog;
using Microsoft.EntityFrameworkCore;

namespace InteractiveHelper.CategoryService;

internal class CategoryService : ICategoryService
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IMapper mapper;

    public CategoryService(IDbContextFactory<MainDbContext> dbContextFactory, IMapper mapper)
    {
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<CategoryModel>> GetCategories()
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var categories = context.Categories.AsQueryable();

        return await categories.Select(category => mapper.Map<CategoryModel>(category)).ToListAsync();
    }

    public async Task<CategoryModel> AddCategory(AddCategoryModel model)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var category = mapper.Map<Category>(model);
        await context.Categories.AddAsync(category);
        await context.SaveChangesAsync();

        return mapper.Map<CategoryModel>(category);
    }

    public async Task<CategoryModel> UpdateCategory(int categoryId, UpdateCategoryModel model)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var category = await GetCategoryByIdFromContext(context, categoryId);

        category = mapper.Map(model, category);

        context.Categories.Update(category);
        context.SaveChanges();

        return mapper.Map<CategoryModel>(category);
    }

    public async Task DeleteCategory(int categoryId)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var category = await GetCategoryByIdFromContext(context, categoryId);

        context.Remove(category);
        context.SaveChanges();
    }

    private static async Task<Category> GetCategoryByIdFromContext(MainDbContext context, int categoryId)
    {
        return await context.Categories.FindAsync(categoryId)
            ?? throw new CommonException(404, $"The category with id:{categoryId} was not found");
    }

    public async Task<IEnumerable<ItemModel>> GetCategoryItems(int categoryId, int offset = 0, int limit = 10)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var category = await GetCategoryByIdFromContext(context, categoryId);

        var items = category.Items.AsQueryable()
            .Skip(Math.Max(offset, 0)).Take(Math.Min(limit, 1000));

        return await items.Select(item => mapper.Map<ItemModel>(item)).ToListAsync();
    }
}
