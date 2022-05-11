using AutoMapper;
using FluentValidation;
using InteractiveHelper.CategoryService.Models;

namespace InteractiveHelper.Api.Controllers.Catalog.Categories.Models;

public class AddCategoryRequest
{
    public string Title { get; set; }
    public class AddCategoryRequestValidator : AbstractValidator<AddCategoryRequest>
    {
        public AddCategoryRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(50).WithMessage("Title is too long.");
        }
    }
    public class AddCategoryRequestProfile : Profile
    {
        public AddCategoryRequestProfile()
        {
            CreateMap<AddCategoryRequest, AddCategoryModel>();
        }
    }
}
