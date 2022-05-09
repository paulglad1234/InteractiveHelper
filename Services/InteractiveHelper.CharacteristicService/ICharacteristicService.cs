using InteractiveHelper.CharactetisricService.Models;

namespace InteractiveHelper.CharactetisricService;

public interface ICharacteristicService
{
    Task<IEnumerable<CharacteristicModel>> GetCategoryCharacteristics(int categoryId);
    Task UpdateCategoryCharacterisrics(int categoryId, IEnumerable<UpdateCharacteristicModel> characteristicModels);
    Task<IEnumerable<ItemCharacteristicModel>> GetItemCharacteristics(int itemId);
    Task UpdateItemCharacteristics(int itemId, IEnumerable<UpdateItemCharacteristicModel> itemCharacteristicModels);
}
