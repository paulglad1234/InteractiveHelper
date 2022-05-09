using AutoMapper;
using InteractiveHelper.CharactetisricService.Models;
using InteractiveHelper.Common.Exceptions;
using InteractiveHelper.Db.Context;
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

        var category = context.Categories.Find(categoryId);
        CommonException.ThrowIfNull(category, "Given category not found.", 404);

        return mapper.Map<IEnumerable<CharacteristicModel>>(category.Characteristics.ToList());
    }

    public async Task<IEnumerable<ItemCharacteristicModel>> GetItemCharacterictics(int itemId)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateCategoryCharacterisrics(int categoryId, IEnumerable<UpdateCharacteristicModel> characteristicModels)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateItemCharacterictics(int itemId, IEnumerable<UpdateItemCharacteristicModel> itemCharactericticModels)
    {
        throw new NotImplementedException();
    }
}
