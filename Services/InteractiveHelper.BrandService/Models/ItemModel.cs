using AutoMapper;
using InteractiveHelper.Db.Entities;

namespace InteractiveHelper.BrandService.Models;

public class ItemModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public float Price { get; set; }
    public byte[] Image { get; set; }
}

public class ItemModelProfile : Profile
{
    public ItemModelProfile()
    {
        CreateMap<Item, ItemModel>();
    }
}
