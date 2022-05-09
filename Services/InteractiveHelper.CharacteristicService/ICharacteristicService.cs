using InteractiveHelper.CharactetisricService.Models;

namespace InteractiveHelper.CharactetisricService;

public interface ICharacteristicService
{
    Task<IEnumerable<CharacteristicModel>> GetCategoryCharacteristics(int categoryId);
    Task UpdateCategoryCharacterisrics(int categoryId, IEnumerable<UpdateCharacteristicModel> characteristicModels);
    Task<IEnumerable<ItemCharacteristicModel>> GetItemCharacterictics(int itemId);
    Task UpdateItemCharacterictics(int itemId, IEnumerable<UpdateItemCharacteristicModel> itemCharactericticModels);
}
