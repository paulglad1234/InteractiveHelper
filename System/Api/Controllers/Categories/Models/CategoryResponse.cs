using AutoMapper;
using InteractiveHelper.CategoryService.Models;

namespace InteractiveHelper.Api.Controllers.Categories.Models
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