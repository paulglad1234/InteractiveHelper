using AutoMapper;
using InteractiveHelper.CharactetisricService.Models;
using InteractiveHelper.Common.Exceptions;
using InteractiveHelper.Db.Context;
using InteractiveHelper.Db.Entities;
using Microsoft.EntityFrameworkCore;

namespace InteractiveHelper.CharactetisricService;

public class CharacteristicService : ICharacteristicService
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IMapper mapper;

    public CharacteristicService(IDbContextFactory<MainDbContext> dbContextFactory, IMapper mapper)
    {
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<CharacteristicModel>> GetCategoryCharacteristics(int categoryId)
    {
        var context = await dbContextFactory.CreateDbContextAsync();

        var category = await context.Categories.FindAsync(categoryId);
        CommonException.ThrowIfNull(category, "Given category not found.", 404);

        var characteristics = category.Characteristics.ToList();

        return mapper.Map<IEnumerable<CharacteristicModel>>(characteristics);
    }

    public async Task<IEnumerable<ItemCharacteristicModel>> GetItemCharacteristics(int itemId)
    {
        var context = await dbContextFactory.CreateDbContextAsync();

        var item = await context.Items.FindAsync(itemId);
        CommonException.ThrowIfNull(item, "Item not found", 404);

        var itemCharacteristics = item.ItemCharacteristics.ToList();

        return mapper.Map<IEnumerable<ItemCharacteristicModel>>(itemCharacteristics);
    }

    public async Task UpdateCategoryCharacterisrics(int categoryId, IEnumerable<UpdateCharacteristicModel> characteristicModels)
    {
        var context = await dbContextFactory.CreateDbContextAsync();
        var category = await context.Categories.FindAsync(categoryId);
        CommonException.ThrowIfNull(category, "Given category not found.", 404);

        foreach (var characteristicModel in characteristicModels)
        {
            if (characteristicModel.Id is null)
            {
                var characteristic = mapper.Map<Characteristic>(characteristicModel);
                characteristic.Category = category;
                context.Characteristics.Add(characteristic);
            }
            else
            {
                var characteristic = context.Characteristics.Find(characteristicModel.Id);
                CommonException.ThrowIfNull(characteristic, $"Characteristic with id={characteristicModel.Id} does not exist", 400);
                characteristic = mapper.Map(characteristicModel, characteristic);
                context.Characteristics.Update(characteristic);
            }
        }

        await context.SaveChangesAsync();
    }

    public async Task UpdateItemCharacteristics(int itemId, IEnumerable<UpdateItemCharacteristicModel> itemCharacteristicModels)
    {   // TODO: rethink this, maybe use Contains<>() method
        var context = await dbContextFactory.CreateDbContextAsync();
        var item = await context.Items.FindAsync(itemId);
        CommonException.ThrowIfNull(item, "Item not found", 404);
        var category = await context.Categories.FindAsync(item.CategoryId);
        CommonException.ThrowIfNull(category, "Item's category does not exist");

        foreach (var itemCharacteristicModel in itemCharacteristicModels)
        {
            // TODO: The following checks may affect performance since they go repeatedly
            var characteristic = await context.Characteristics.FindAsync(itemCharacteristicModel.CharacteristicId);
            CommonException.ThrowIfNull(characteristic, 
                $"Characteristic ({itemCharacteristicModel.CharacteristicId}) does not exist. Add it to the category first.");
            CommonException.ThrowIf(characteristic.CategoryId != category.Id,
                $"Characteristic ({itemCharacteristicModel.CharacteristicId}) does not belong to item's ({itemId}) category ({category.Id}");

            var itemCharacteristic = await context.ItemCharacteristics.FindAsync(itemId, characteristic.Id);
            if (itemCharacteristic is null)
            {
                itemCharacteristic = mapper.Map<ItemCharacteristic>(itemCharacteristicModel);
                itemCharacteristic.Item = item;
                context.ItemCharacteristics.Add(itemCharacteristic);
            } 
            else
            {
                itemCharacteristic = mapper.Map(itemCharacteristicModel, itemCharacteristic);
                context.ItemCharacteristics.Update(itemCharacteristic);
            }
        }

        await context.SaveChangesAsync();
    }
}
