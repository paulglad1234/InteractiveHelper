using AutoMapper;
using InteractiveHelper.CategoryService.Models;

namespace InteractiveHelper.Api.Controllers.Catalog.Categories.Models;

public class CategoryItemResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public float Price { get; set; }
    public byte[] Image { get; set; }
}

public class ItemResponseProfile : Profile
{
    public ItemResponseProfile()
    {
        CreateMap<ItemModel, CategoryItemResponse>();
    }
}
