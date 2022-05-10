using AutoMapper;
using FluentValidation;
using InteractiveHelper.Db.Entities.Catalog;

namespace InteractiveHelper.CategoryService.Models;

public class UpdateCategoryModel
{
    public string Title { get; set; }
}

public class UpdateCategoryModelValidator : AbstractValidator<UpdateCategoryModel>
{
    public UpdateCategoryModelValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(50).WithMessage("Title is too long.");
    }
}

public class UpdateCategoryModelProfile : Profile
{
    public UpdateCategoryModelProfile()
    {
        CreateMap<UpdateCategoryModel, Category>();
    }
}