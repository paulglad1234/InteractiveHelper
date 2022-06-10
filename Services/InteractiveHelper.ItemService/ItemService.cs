using AutoMapper;
using InteractiveHelper.ItemService.Models;
using InteractiveHelper.Common.Exceptions;
using InteractiveHelper.Db.Context;
using InteractiveHelper.Db.Entities.Catalog;
using Microsoft.EntityFrameworkCore;

namespace InteractiveHelper.ItemService;

internal class ItemService : IItemService
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IMapper mapper;

    public ItemService(IDbContextFactory<MainDbContext> dbContextFactory, IMapper mapper)
    {
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<ItemModel>> GetItems(int offset = 0, int limit = 10)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var items = context.Items
            .Skip(Math.Max(offset, 0))
            .Take(Math.Max(0, Math.Min(limit, 1000)));

        return mapper.Map<IEnumerable<ItemModel>>(await items.ToListAsync());
    }

    public async Task<ItemModel> GetItem(int itemId)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        return mapper.Map<ItemModel>(await GetItemByIdFromContext(context, itemId));
    }

    public async Task<ItemModel> AddItem(AddItemModel model)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        CommonException.ThrowIfNull(context.Brands.Find(model.BrandId), "Given brand wasn't found", 400);
        CommonException.ThrowIfNull(context.Categories.Find(model.BrandId), "Given category wasn't found", 400);

        var item = mapper.Map<Item>(model);
        await context.Items.AddAsync(item);
        await context.SaveChangesAsync();

        return mapper.Map<ItemModel>(item);
    }

    public async Task<ItemModel> UpdateItem(int itemId, UpdateItemModel model)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var item = await GetItemByIdFromContext(context, itemId);

        item = mapper.Map(model, item);

        context.Items.Update(item);
        context.SaveChanges();

        return mapper.Map<ItemModel>(item);
    }

    public async Task DeleteItem(int itemId)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var item = await GetItemByIdFromContext(context, itemId);

        context.Remove(item);
        context.SaveChanges();
    }

    private static async Task<Item> GetItemByIdFromContext(MainDbContext context, int itemId)
    {
        return await context.Items.FindAsync(itemId)
            ?? throw new CommonException(404, $"The item with id:{itemId} was not found");
    }
}
