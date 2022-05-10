using AutoMapper;
using InteractiveHelper.CharactetisricService.Models;
using InteractiveHelper.Common.Exceptions;
using InteractiveHelper.Db.Context;
using InteractiveHelper.Db.Entities.Catalog;
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
        using var context = await dbContextFactory.CreateDbContextAsync();

        var category = await context.Categories.FindAsync(categoryId);
        CommonException.ThrowIfNull(category, "Given category not found.", 404);

        var characteristics = category.Characteristics.ToList();

        return mapper.Map<IEnumerable<CharacteristicModel>>(characteristics);
    }

    public async Task<IEnumerable<ItemCharacteristicModel>> GetItemCharacteristics(int itemId)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var item = await context.Items.FindAsync(itemId);
        CommonException.ThrowIfNull(item, "Item not found", 404);

        var itemCharacteristics = item.ItemCharacteristics.ToList();

        return mapper.Map<IEnumerable<ItemCharacteristicModel>>(itemCharacteristics);
    }

    public async Task UpdateCategoryCharacterisrics(int categoryId, IEnumerable<UpdateCharacteristicModel> characteristicModels)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();
        var category = await context.Categories.FindAsync(categoryId);
        CommonException.ThrowIfNull(category, "Given category not found.", 404);

        // Remove deleted characteristics
        context.Characteristics.RemoveRange(
            category.Characteristics.AsQueryable().Where( // maybe should remove Where and just remove all category's chars
                                            // since characteristicModels should contain all of them
                characteristic => !characteristicModels.Any(
                    model => model.Id == characteristic.Id))); // not sure if int?.Equals(int) won't always return false

        // Create added characteristics
        var characteristicsToAdd = mapper.Map<IEnumerable<Characteristic>>(
            characteristicModels.Where(model => model.Id is null));
        foreach (var characteristic in characteristicsToAdd) 
            characteristic.Category = category;
        context.Characteristics.AddRange(characteristicsToAdd);

        // Update changed characteristics
        foreach (var characteristicModel in characteristicModels
            .Where(model => model.Id is not null)) // The characteristic is old and needs to be updated
        {
            var characteristic = category.Characteristics.AsQueryable()
                .FirstOrDefault(ch => ch.Id.Equals(characteristicModel.Id));
            CommonException.ThrowIfNull(characteristic, $"Characteristic with id={characteristicModel.Id} does not exist", 400);
            characteristic = mapper.Map(characteristicModel, characteristic);
            context.Characteristics.Update(characteristic);
        }

        await context.SaveChangesAsync();
    }

    public async Task UpdateItemCharacteristics(int itemId, IEnumerable<UpdateItemCharacteristicModel> itemCharacteristicModels)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();
        var item = await context.Items.FindAsync(itemId);
        CommonException.ThrowIfNull(item, "Item not found", 404);
        var category = item.Category; // Thanks, lazy loading
        CommonException.ThrowIfNull(category, "Item's category does not exist");

        // Remove deleted
        context.ItemCharacteristics.RemoveRange(item.ItemCharacteristics.AsQueryable()
            .Where(ic => !itemCharacteristicModels.Any(model => model.CharacteristicId == ic.CharacteristicId)));

        foreach (var itemCharacteristicModel in itemCharacteristicModels)
        {
            // Search characteristic in category
            var characteristic = category.Characteristics.AsQueryable().FirstOrDefault(
                ch => ch.Id.Equals(itemCharacteristicModel.CharacteristicId));
            CommonException.ThrowIfNull(characteristic, 
                $"Characteristic ({itemCharacteristicModel.CharacteristicId}) does not exist " +
                $"or does not belong to item's ({itemId}) category ({category.Id}.\r\n" +
                $"Add it to the category first.");

            var itemCharacteristic = item.ItemCharacteristics.AsQueryable()
                .FirstOrDefault(ic => ic.CharacteristicId.Equals(characteristic.Id));

            // Add if does not exist
            if (itemCharacteristic is null)
            {
                itemCharacteristic = mapper.Map<ItemCharacteristic>(itemCharacteristicModel);
                itemCharacteristic.Item = item;
                context.ItemCharacteristics.Add(itemCharacteristic);
            }
            // Update if does
            else
            {
                itemCharacteristic = mapper.Map(itemCharacteristicModel, itemCharacteristic);
                context.ItemCharacteristics.Update(itemCharacteristic);
            }
        }

        await context.SaveChangesAsync();
    }
}
