namespace InteractiveHelper.CatalogAdminPanel;

using System.Threading.Tasks;

public interface IItemService
{
    Task<IEnumerable<ItemOutModel>> GetItems(int offset = 0, int limit = 10);
    Task<ItemOutModel> GetItem(int itemId);
    Task AddItem(ItemInModel model);
    Task UpdateItem(int itemId, ItemInModel model);
    Task DeleteItem(int itemId);

    Task<IEnumerable<BrandModel>> GetBrandsList();
    Task<IEnumerable<CategoryModel>> GetCategoriesList();
}
