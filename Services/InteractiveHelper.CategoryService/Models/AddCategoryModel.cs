using AutoMapper;
using FluentValidation;
using InteractiveHelper.Db.Entities.Catalog;

namespace InteractiveHelper.CategoryService.Models;

public class AddCategoryModel
{
    public string Title { get; set; }
}

public class AddCategoryModelValidator : AbstractValidator<AddCategoryModel>
{
    public AddCategoryModelValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(50).WithMessage("Title is too long.");
    }
}

public class AddCategoryModelProfile : Profile
{
    public AddCategoryModelProfile()
    {
        CreateMap<AddCategoryModel, Category>();
    }
}
