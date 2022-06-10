using AutoMapper;
using InteractiveHelper.Db.Entities.Catalog;

namespace InteractiveHelper.CatalogServices.Categories.Models;

public class CategoryModel
{
    public int Id { get; set; }
    public string Title { get; set; }
}

public class CategoryModelProfile : Profile
{
    public CategoryModelProfile()
    {
        CreateMap<Category, CategoryModel>();
    }
}
