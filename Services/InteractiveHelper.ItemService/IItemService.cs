using InteractiveHelper.ItemService.Models;

namespace InteractiveHelper.ItemService;

public interface IItemService
{
    Task<IEnumerable<ItemModel>> GetItems(int offset = 0, int limit = 10);
    Task<ItemModel> GetItem(int itemId);
    Task<ItemModel> AddItem(AddItemModel model);
    Task<ItemModel> UpdateItem(int itemId, UpdateItemModel model);
    Task DeleteItem(int itemId);
}
