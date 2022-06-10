using AutoMapper;
using InteractiveHelper.CatalogServices.Items.Models;

namespace InteractiveHelper.Api.Controllers.Catalog.Brands.Models;

public class BrandsItemResponse
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
        CreateMap<ItemModel, BrandsItemResponse>();
    }
}
