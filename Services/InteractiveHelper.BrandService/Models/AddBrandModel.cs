using AutoMapper;
using FluentValidation;
using InteractiveHelper.Db.Entities.Catalog;

namespace InteractiveHelper.BrandService.Models;

public class AddBrandModel
{
    public string Name { get; set; }
}

public class AddBrandModelValidator : AbstractValidator<AddBrandModel>
{
    public AddBrandModelValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(50).WithMessage("Name is too long.");
    }
}

public class AddBrandModelProfile : Profile
{
    public AddBrandModelProfile()
    {
        CreateMap<AddBrandModel, Brand>();
    }
}
