using AutoMapper;
using InteractiveHelper.BrandService.Models;

namespace InteractiveHelper.Api.Controllers.Catalog.Brands.Models
{
    public class BrandResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class BrandResponseProfile : Profile
    {
        public BrandResponseProfile()
        {
            CreateMap<BrandModel, BrandResponse>();
        }
    }
}