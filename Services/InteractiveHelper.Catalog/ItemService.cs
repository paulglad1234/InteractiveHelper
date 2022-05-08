using AutoMapper;
using InteractiveHelper.Catalog.Models;
using InteractiveHelper.Common.Exceptions;
using InteractiveHelper.Db.Context;
using InteractiveHelper.Db.Entities;
using Microsoft.EntityFrameworkCore;

namespace InteractiveHelper.Catalog;

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

        var items = context.Items.AsQueryable();

        items = items.Skip(Math.Max(offset, 0)).Take(Math.Min(limit, 1000));

        return items.Select(item => mapper.Map<ItemModel>(item));
    }

    public async Task<ItemModel> GetItemById(int itemId)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        return mapper.Map<ItemModel>(await GetItemByIdFromContext(context, itemId));
    }

    public async Task<ItemModel> AddItem(AddItemModel model)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

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
        return await context.Items.FirstOrDefaultAsync(i => i.Id.Equals(itemId))
            ?? throw new CommonException($"The item with id:{itemId} was not found");
    }
}
