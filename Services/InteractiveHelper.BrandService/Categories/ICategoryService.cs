using InteractiveHelper.CatalogServices.Categories.Models;

namespace InteractiveHelper.CatalogServices.Categories;

public interface ICategoryService
{
    Task<IEnumerable<CategoryModel>> GetCategories();
    Task<IEnumerable<ItemModel>> GetCategoryItems(int categoryId, int offset = 0, int limit = 10);
    Task<CategoryModel> AddCategory(AddCategoryModel model);
    Task<CategoryModel> UpdateCategory(int CategoryId, UpdateCategoryModel model);
    Task DeleteCategory(int CategoryId);
}
