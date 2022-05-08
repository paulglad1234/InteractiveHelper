using InteractiveHelper.Catalog.Models;

namespace InteractiveHelper.Catalog;

public interface IItemService
{
    Task<IEnumerable<ItemModel>> GetItems(int offset = 0, int limit = 10);
    Task<ItemModel> GetItemById(int itemId);
    Task<ItemModel> AddItem(AddItemModel model);
    Task<ItemModel> UpdateItem(int itemId, UpdateItemModel model);
    Task DeleteItem(int itemId);
}
