using AutoMapper;
using InteractiveHelper.Db.Entities.Catalog;

namespace InteractiveHelper.ItemService.Models;

public class ItemModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public float Price { get; set; }
    public byte[] Image { get; set; }

    public int BrandId { get; set; }
    public string Brand { get; set; }
    public int CategoryId { get; set; }
    public string Category { get; set; }
}

public class ItemModelProfile : Profile
{
    public ItemModelProfile()
    {
        CreateMap<Item, ItemModel>()
            .ForMember(res => res.Brand, opt => opt.MapFrom(src => src.Brand.Name))
            .ForMember(res => res.Category, opt => opt.MapFrom(src => src.Category.Title));
    }
}
