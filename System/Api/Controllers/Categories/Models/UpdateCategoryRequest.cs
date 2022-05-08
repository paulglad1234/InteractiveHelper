using AutoMapper;
using FluentValidation;
using InteractiveHelper.CategoryService.Models;

namespace InteractiveHelper.Api.Controllers.Categories.Models;

public class UpdateCategoryRequest
{
    public string Title { get; set; }
    public class UpdateCategoryRequestValidator : AbstractValidator<UpdateCategoryRequest>
    {
        public UpdateCategoryRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(50).WithMessage("Title is too long.");
        }
    }
    public class UpdateCategoryRequestProfile : Profile
    {
        public UpdateCategoryRequestProfile()
        {
            CreateMap<UpdateCategoryRequest, UpdateCategoryModel>();
        }
    }
}
