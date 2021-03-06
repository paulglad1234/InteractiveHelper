using AutoMapper;
using InteractiveHelper.CatalogServices.Categories.Models;

namespace InteractiveHelper.Api.Controllers.Catalog.Categories.Models
{
    public class CategoryResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
    public class CategoryResponseProfile : Profile
    {
        public CategoryResponseProfile()
        {
            CreateMap<CategoryModel, CategoryResponse>();
        }
    }
}