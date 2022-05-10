using AutoMapper;
using InteractiveHelper.Db.Entities.Catalog;

namespace InteractiveHelper.BrandService.Models;

public class BrandModel
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class BrandModelProfile : Profile
{
    public BrandModelProfile()
    {
        CreateMap<Brand, BrandModel>();
    }
}
